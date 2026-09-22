namespace TextTranslator.Models
{
    public class OcrImportResult
    {
        public string RawText { get; set; } = "";
        public List<string> Words { get; set; } = new();
    }
}
