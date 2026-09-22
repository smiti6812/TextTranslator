using TextTranslator.Models;

namespace TextTranslator.Data.Interfaces
{
    public interface IQuizService
    {
        QuizQuestion? GenerateQuestion(List<VocabularyItem> items, bool sourceToTarget = true);
        void RegisterAnswer(VocabularyItem item, bool isCorrect);
        QuizSessionResult CreateResult(int total, int correct);
    }
}
