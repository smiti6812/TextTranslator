using SQLite;

namespace TextTranslator.Data.Interfaces
{
    public interface IDatabaseService
    {
        Task<SQLiteAsyncConnection> GetConnectionAsync();
    }
}
