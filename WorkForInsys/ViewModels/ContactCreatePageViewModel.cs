using Prism.Commands;
using Prism.Mvvm;
using WorkForInsys.Models;
using Xamarin.Forms;

namespace WorkForInsys.ViewModels
{
    // https://docs.microsoft.com/pl-pl/xamarin/xamarin-forms/enterprise-application-patterns/validation
    // https://docs.microsoft.com/pl-pl/xamarin/get-started/quickstarts/database?pivots=windows

    public class ContactCreatePageViewModel : BindableBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public DelegateCommand SaveContactCommand { get; private set; }

        public ContactCreatePageViewModel()
        {
            SaveContactCommand = new DelegateCommand(async () =>
            {
                ContactModel contact = new ContactModel()
                {
                    Name = Name,
                    Surname = Surname,
                    PhoneNumber = PhoneNumber,
                    Email = Email,
                    Address = Address
                };
                await App.Database.SaveContactAsync(contact);

                await Shell.Current.Navigation.PopAsync();
            });
        }
    }
}