using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

using TextTranslator.Data.Interfaces;
using TextTranslator.Models;

namespace TextTranslator.Services
{
    public class TranslationService(HttpClient http_Client, ISettingsService settingsServ) : ITranslationService
    {
        private readonly HttpClient httpClient = http_Client;
        private readonly ISettingsService settingsService = settingsServ;

        public async Task<TranslationResult> TranslateAsync(string text, string source, string target)
        {
            var settings = settingsService.Get();

            if (string.IsNullOrWhiteSpace(settings.TranslationEndpoint))
            {
                return new TranslationResult
                {
                    SourceText = text,
                    TranslatedText = "",
                    SourceLanguage = source,
                    TargetLanguage = target
                };
            }

            var payload = new
            {
                text,
                source,
                target
            };

            var json = JsonSerializer.Serialize(payload);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(settings.TranslationEndpoint, content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadFromJsonAsync<TranslationResponse>();

            // Elvárt válasz példa:
            // { "translatedText": "ház" }
            /*
            using var doc = JsonDocument.Parse(responseJson);
            var translatedText = doc.RootElement.GetProperty("translatedText").GetString() ?? "";
            */
            return new TranslationResult
            {
                SourceText = text,
                TranslatedText = responseJson.Translation,
                SourceLanguage = source,
                TargetLanguage = target
            };
        }
    }
}
