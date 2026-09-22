using System.Text.RegularExpressions;

namespace TextTranslator.Helpers
{
    public static class WordCleaner
    {
        public static List<string> ExtractDistinctWords(string text, string languageCode)
        {
            var excluded = new List<string>();//GetExcludedWords(languageCode);

            return Regex.Matches(text ?? "", @"\p{L}+")
                .Select(m => m.Value.Trim())
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .Where(w => !excluded.Contains(w, StringComparer.OrdinalIgnoreCase))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(x => x)
                .ToList();
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
}
