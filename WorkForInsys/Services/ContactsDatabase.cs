using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using AddressBook.Models;

namespace AddressBook.Services
{
    public class ContactsDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public ContactsDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ContactModel>().Wait();
        }

        public Task<List<ContactModel>> GetContactsAsync()
        {
            return _database.Table<ContactModel>().ToListAsync();
        }

        public Task<ContactModel> GetContactAsync(int id)
        {
            return _database.Table<ContactModel>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveContactAsync(ContactModel contact)
        {
            if (contact.ID != 0)
            {
                return _database.UpdateAsync(contact);
            }
            else
            {
                return _database.InsertAsync(contact);
            }
        }

        public Task<int> DeleteContactAsync(ContactModel contact)
        {
            return _database.DeleteAsync(contact);
        }
    }
}