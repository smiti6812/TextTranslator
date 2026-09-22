namespace TextTranslator.Models
{
    public class TranslationResult
    {
        public string SourceText { get; set; } = "";
        public string TranslatedText { get; set; } = "";
        public string SourceLanguage { get; set; } = "";
        public string TargetLanguage { get; set; } = "";
    }
}
