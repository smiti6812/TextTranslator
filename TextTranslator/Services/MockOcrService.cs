namespace TextTranslator.Services
{
    /*
    public class MockOcrService : IOcrService
    {
        public Task<OcrImportResult> ExtractTextAsync(string filePath, string languageCode)
        {
            string rawText = """
        Het huis is groot. De man loopt naar school.
        Een kind leest een boek.
        """;

            var excluded = GetExcludedWords(languageCode);

            var words = Regex.Matches(rawText, @"\p{L}+")
                .Select(m => m.Value.Trim())
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .Where(w => !excluded.Contains(w, StringComparer.OrdinalIgnoreCase))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(x => x)
                .ToList();

            return Task.FromResult(new OcrImportResult
            {
                RawText = rawText,
                Words = words
            });
        }

        private static List<string> GetExcludedWords(string languageCode)
        {
            return languageCode.ToLower() switch
            {
                "nl" => new() { "de", "het", "een" },
                "en" => new() { "the", "a", "an" },
                "de" => new() { "der", "die", "das", "ein", "eine" },
                "hu" => new() { "a", "az" },
                _ => new()
            };
        }
    }
    */
}
