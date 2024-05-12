using SQLite;

namespace SQLforPika
{
    public class Database
    {
        private SQLiteAsyncConnection _database;
        private readonly string _databasePath;
        private async Task Init()
        {
            if (_database != null)
            {
                return;
            }

            var options = new SQLiteConnectionString(_databasePath, true, "password", postKeyAction: c =>
            {
                c.Execute("PRAGMA cipher_compatibility = 2");
            });
            _database = new SQLiteAsyncConnection(options);
            await _database.CreateTableAsync<Person>();
        }

        public Database(string dbPath)
        {
            _databasePath = dbPath;
        }

        public async Task<List<Person>> GetPeopleAsync()
        {
            await Init();

            return await _database.Table<Person>().ToListAsync();
        }

        public async Task<int> SavePersonAsync(Person person)
        {
            await Init();

            return await _database.InsertAsync(person);
        }

        public async Task<int> UpdatePersonAsync(Person person)
        {
            await Init();

            return await _database.UpdateAsync(person);
        }

        public async Task<int> DeletePersonAsync(Person person)
        {
            await Init();

            return await _database.DeleteAsync(person);
        }

        public async Task<List<Person>> QuerySubscribedAsync()
        {
            await Init();

            return await _database.QueryAsync<Person>("SELECT * FROM Person");
        }

       
        public async Task<int> SortData(Person person)
        {
            await Init();

            return await _database.InsertAsync(person);
            // Update your data source with sortedProducts
        }
    }
}

