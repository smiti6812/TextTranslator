using System.Net.Http.Headers;
using System.Text.Json;

using TextTranslator.Data.Interfaces;
using TextTranslator.Helpers;
using TextTranslator.Models;

namespace TextTranslator.Services
{
    public class OcrSpaceOcrService(HttpClient http_Client, ISettingsService settingsServ) : IOcrService
    {
        private readonly HttpClient httpClient = http_Client;
        private readonly ISettingsService settingsService = settingsServ;

        public async Task<OcrImportResult> ExtractTextAsync(FileResult file, string languageCode)
        {
            var settings = settingsService.Get();

            if (string.IsNullOrWhiteSpace(settings.OcrApiKey))
            {
                throw new InvalidOperationException("OCR API kulcs nincs beállítva.");
            }

            using var stream = await file.OpenReadAsync();
            using var form = new MultipartFormDataContent();
            using var streamContent = new StreamContent(stream);

            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            form.Add(streamContent, "file", file.FileName);
            form.Add(new StringContent(settings.OcrApiKey), "apikey");

            // OCR.Space nyelvkód más lehet, itt egyszerű minta:
            form.Add(new StringContent(MapLanguageCode(languageCode)), "language");
            form.Add(new StringContent("true"), "isOverlayRequired");

            var response = await httpClient.PostAsync("https://api.ocr.space/parse/image", form);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            string parsedText = ParseOcrSpaceText(json);
            var words = WordCleaner.ExtractDistinctWords(parsedText, languageCode);

            return new OcrImportResult
            {
                RawText = parsedText,
                Words = words
            };
        }

        private static string ParseOcrSpaceText(string json)
        {
            using var doc = JsonDocument.Parse(json);

            if (!doc.RootElement.TryGetProperty("ParsedResults", out var parsedResults))
            {
                return "";
            }

            var lines = new List<string>();

            foreach (var result in parsedResults.EnumerateArray())
            {
                if (result.TryGetProperty("ParsedText", out var parsedTextElement))
                {
                    var text = parsedTextElement.GetString();
                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        lines.Add(text);
                    }
                }
            }

            return string.Join(Environment.NewLine, lines);
        }

        private static string MapLanguageCode(string languageCode)
        {
            return languageCode.ToLowerInvariant() switch
            {
                "en" => "eng",
                "de" => "ger",
                "nl" => "dut",
                "fr" => "fre",
                "es" => "spa",
                _ => "eng"
            };
        }
    }
}
