namespace TextTranslator.Services
{
    /*
    public class ApiOcrService(HttpClient http_Client) : IOcrService
    {
        private readonly HttpClient httpClient = http_Client;

        public async Task<OcrImportResult> ExtractTextAsync(string filePath, string languageCode)
        {
            // Itt olvasod be a fájlt és küldöd OCR API-nak.
            // Most csak mintahelykitöltő:
            var bytes = await File.ReadAllBytesAsync(filePath);

            // Például multipart/form-data vagy base64 kérés lehet.
            // A konkrét API-tól függ.
            var base64 = Convert.ToBase64String(bytes);

            var payload = new
            {
                imageBase64 = base64,
                language = languageCode
            };

            var json = JsonSerializer.Serialize(payload);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Példa endpoint
            // var response = await _httpClient.PostAsync("https://your-ocr-api/ocr", content);
            // response.EnsureSuccessStatusCode();
            // var responseJson = await response.Content.ReadAsStringAsync();

            // TODO: API válasz parse
            string rawText = "Mockolt API OCR válasz szövege";

            var words = Regex.Matches(rawText, @"\p{L}+")
                .Select(m => m.Value)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            return new OcrImportResult
            {
                RawText = rawText,
                Words = words
            };
        }
    
    }
    */
}
