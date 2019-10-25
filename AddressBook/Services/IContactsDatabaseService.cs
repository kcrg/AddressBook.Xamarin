using AddressBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressBook.Services
{
    public interface IContactsDatabaseService
    {
        Task<List<ContactModel>> GetContactsAsync();
        Task<ContactModel> GetContactAsync(int id);
        Task<int> SaveContactAsync(ContactModel contact);
        Task<int> DeleteContactAsync(ContactModel contact);
    }
}