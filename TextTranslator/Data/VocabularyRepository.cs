using TextTranslator.Data.Interfaces;
using TextTranslator.Models;

namespace TextTranslator.Data
{
    public class VocabularyRepository : IVocabularyRepository
    {
        private readonly IDatabaseService databaseService;

        public VocabularyRepository(IDatabaseService databaseServ) => databaseService = databaseServ;

        public async Task<List<VocabularyItem>> GetAllAsync()
        {
            var db = await databaseService.GetConnectionAsync();
            return await db.Table<VocabularyItem>()
                           .OrderBy(x => x.SourceWord)
                           .ToListAsync();
        }

        public async Task<VocabularyItem?> GetByIdAsync(int id)
        {
            var db = await databaseService.GetConnectionAsync();
            return await db.Table<VocabularyItem>()
                           .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<VocabularyItem>> SearchAsync(string searchTerm)
        {
            var db = await databaseService.GetConnectionAsync();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllAsync();
            }

            searchTerm = searchTerm.Trim().ToLower();

            var all = await db.Table<VocabularyItem>().ToListAsync();

            return all.Where(x =>
                    x.SourceWord.ToLower().Contains(searchTerm) ||
                    x.TargetWord.ToLower().Contains(searchTerm) ||
                    x.PartOfSpeech.ToLower().Contains(searchTerm) ||
                    x.Note.ToLower().Contains(searchTerm))
                .OrderBy(x => x.SourceWord)
                .ToList();
        }

        public async Task<List<VocabularyItem>> GetBySourceLanguageAsync(string sourceLanguage)
        {
            var db = await databaseService.GetConnectionAsync();
            return await db.Table<VocabularyItem>()
                .Where(x => x.SourceLanguage == sourceLanguage)
                .ToListAsync();
        }

        public async Task<int> AddAsync(VocabularyItem item)
        {
            var db = await databaseService.GetConnectionAsync();
            item.SourceLanguage = item.SourceLanguage.Trim();
            item.SourceWord = item.SourceWord.Trim();
            item.TargetLanguage = item.TargetLanguage.Trim();
            item.TargetWord = item.TargetWord.Trim();
            item.PartOfSpeech = item.PartOfSpeech.Trim();
            item.Note = item.Note.Trim();
            item.SourceText = item.SourceText.Trim();

            var existingItems = await db.Table<VocabularyItem>().ToListAsync();
            var exists = existingItems.Any(x =>
                string.Equals(x.SourceLanguage.Trim(), item.SourceLanguage, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(x.SourceWord.Trim(), item.SourceWord, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(x.TargetLanguage.Trim(), item.TargetLanguage, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(x.TargetWord.Trim(), item.TargetWord, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(x.PartOfSpeech.Trim(), item.PartOfSpeech, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(x.Note.Trim(), item.Note, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(x.SourceText.Trim(), item.SourceText, StringComparison.OrdinalIgnoreCase));

            if (exists)
            {
                return 0;
            }

            item.CreatedAtUtc = DateTime.UtcNow;
            item.UpdatedAtUtc = null;
            return await db.InsertAsync(item);
        }

        public async Task<int> UpdateAsync(VocabularyItem item)
        {
            var db = await databaseService.GetConnectionAsync();
            item.UpdatedAtUtc = DateTime.UtcNow;
            return await db.UpdateAsync(item);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var db = await databaseService.GetConnectionAsync();
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return 0;
            }

            return await db.DeleteAsync(entity);
        }
    }
}
