﻿using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkForInsys.Models;

namespace WorkForInsys.Data
{
    public class ContactsDatabase // move to services
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