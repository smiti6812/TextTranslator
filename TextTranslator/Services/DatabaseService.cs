
using SQLite;

using TextTranslator.Data.Interfaces;

namespace TextTranslator.Services
{
    public class DatabaseService : IDatabaseService
    {
        private SQLiteAsyncConnection? connection;
        private readonly SemaphoreSlim semaphore = new(1, 1);

        public async Task<SQLiteAsyncConnection> GetConnectionAsync()
        {
            if (connection != null)
            {
                return connection;
            }

            await semaphore.WaitAsync();
            try
            {
                if (connection == null)
                {
                    var dbPath = Path.Combine(FileSystem.AppDataDirectory, "languageapp.db3");
                    connection = new SQLiteAsyncConnection(dbPath);
                    await connection.CreateTableAsync<Models.VocabularyItem>();
                }
            }
            finally
            {
                semaphore.Release();
            }

            return connection;
        }
    }
}
