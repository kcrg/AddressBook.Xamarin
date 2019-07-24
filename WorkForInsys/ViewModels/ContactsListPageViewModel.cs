using Prism.Commands;
using Prism.Mvvm;
using Xamarin.Forms;

namespace WorkForInsys.ViewModels
{
    public class ContactsListPageViewModel : BindableBase
    {
        public DelegateCommand AddContactCommand { get; private set; }

        public ContactsListPageViewModel()
        {
            AddContactCommand = new DelegateCommand(async () =>
            {
                await Shell.Current.GoToAsync("contactlist/contactcreate");
            });
        }
    }
}