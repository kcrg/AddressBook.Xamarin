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
        public DelegateCommand SaveContactCommand { get; private set; }

        public ContactCreatePageViewModel()
        {
            SaveContactCommand = new DelegateCommand(async () =>
            {
                ContactModel contact = new ContactModel()
                {
                    Name = "Kacper",
                    Surname = "Tryniecki",
                    PhoneNumber = "733428869",
                    Email = "kacper@tryniecki.com",
                    Address = "Rzeszów"
                };
                await App.Database.SaveContactAsync(contact);

                await Shell.Current.Navigation.PopAsync();
            });
        }
    }
}