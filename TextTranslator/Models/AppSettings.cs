namespace TextTranslator.Models
{
    public class AppSettings
    {
        public string OcrApiKey { get; set; } = "K88324611088957";
        public string TranslationApiKey { get; set; } = "";
        public string TranslationEndpoint { get; set; } = "http://localhost:5001/translate";
        public bool AutoTranslateOnImport { get; set; } = true;
    }
}
