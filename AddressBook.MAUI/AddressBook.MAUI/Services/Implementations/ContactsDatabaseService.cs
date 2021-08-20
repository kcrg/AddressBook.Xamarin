using AddressBook.MAUI.Models;
using SQLite;

namespace AddressBook.MAUI.Services.Implementations
{
    public class ContactsDatabaseService : IContactsDatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public ContactsDatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ContactModel>().Wait();
        }

        public async Task<List<ContactModel>> GetContactsAsync()
        {
            return await _database.Table<ContactModel>().ToListAsync().ConfigureAwait(false);
        }

        public async Task<ContactModel> GetContactAsync(int id)
        {
            return await _database.Table<ContactModel>().Where(i => i.ID == id).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<int> SaveContactAsync(ContactModel contact)
        {
            return contact.ID != 0
                ? await _database.UpdateAsync(contact).ConfigureAwait(false)
                : await _database.InsertAsync(contact).ConfigureAwait(false);
        }

        public async Task<int> DeleteContactAsync(ContactModel contact)
        {
            return await _database.DeleteAsync(contact).ConfigureAwait(false);
        }
    }
}