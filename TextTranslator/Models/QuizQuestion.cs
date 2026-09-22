namespace TextTranslator.Models
{
    public class QuizQuestion
    {
        public VocabularyItem? SourceItem { get; set; }
        public string Prompt { get; set; } = "";
        public List<QuizAnswerOption> Options { get; set; } = new();
    }
}
