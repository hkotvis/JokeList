using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace JokeList
{
    public class JokeListDatabase 
    {
        readonly SQLiteAsyncConnection database;

        public JokeListDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<JokeItem>().Wait();
        }

        public Task<List<JokeItem>> GetItemsAsync()
        {
            return database.Table<JokeItem>().ToListAsync();
        }

        public Task<List<JokeItem>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<JokeItem>("SELECT * FROM [JokeItem] WHERE [Delete] = 0");
        }

        public Task<JokeItem> GetItemAsync(int id)
        {
            return database.Table<JokeItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(JokeItem item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(JokeItem item)
        {
            return database.DeleteAsync(item);
        }
    }
}

