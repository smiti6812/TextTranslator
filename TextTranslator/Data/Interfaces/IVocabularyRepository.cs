using TextTranslator.Models;

namespace TextTranslator.Data.Interfaces
{
    public interface IVocabularyRepository
    {
        Task<List<VocabularyItem>> GetAllAsync();
        Task<VocabularyItem?> GetByIdAsync(int id);
        Task<List<VocabularyItem>> SearchAsync(string searchTerm);
        Task<List<VocabularyItem>> GetBySourceLanguageAsync(string sourceLanguage);
        Task<int> AddAsync(VocabularyItem item);
        Task<int> UpdateAsync(VocabularyItem item);
        Task<int> DeleteAsync(int id);
    }
}
