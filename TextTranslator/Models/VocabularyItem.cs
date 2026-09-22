using SQLite;

namespace TextTranslator.Models
{
    public class VocabularyItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string SourceLanguage { get; set; } = "";   // nl, en, de...
        public string SourceWord { get; set; } = "";
        public string TargetLanguage { get; set; } = "hu";
        public string TargetWord { get; set; } = "";
        public string PartOfSpeech { get; set; } = "";     // főnév, ige...
        public string Note { get; set; } = "";
        public string SourceText { get; set; } = "";       // eredeti OCR szöveg/részlet
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAtUtc { get; set; }
        public int SuccessCount { get; set; }
        public int FailCount { get; set; }
        public DateTime? LastReviewedUtc { get; set; }
    }
}
