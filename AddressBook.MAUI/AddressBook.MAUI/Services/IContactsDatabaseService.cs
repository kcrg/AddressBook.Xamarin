using AddressBook.MAUI.Models;

namespace AddressBook.MAUI.Services
{
    public interface IContactsDatabaseService
    {
        Task<List<ContactModel>> GetContactsAsync();
        Task<ContactModel> GetContactAsync(int id);
        Task<int> SaveContactAsync(ContactModel contact);
        Task<int> DeleteContactAsync(ContactModel contact);
    }
}