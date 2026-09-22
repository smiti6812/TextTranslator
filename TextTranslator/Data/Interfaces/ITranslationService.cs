using TextTranslator.Models;

namespace TextTranslator.Data.Interfaces
{

    public interface ITranslationService
    {
        Task<TranslationResult> TranslateAsync(string text, string sourceLanguage, string targetLanguage);
    }
}
