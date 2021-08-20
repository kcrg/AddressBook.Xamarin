using AddressBook.MAUI.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AddressBook.MAUI.ViewModels
{
    public class ContactsListPageViewModel : BindableObject
    {
        //private bool _isLoading = true;

        public bool IsLoading
        {
            get; set;
        }

        public ObservableCollection<ContactModel> ContactsList { get; }
        public ICommand AddContactCommand { get; }
        public ICommand SettingsCommand { get; }
        // ICommand<string>? DeleteContactCommand { get; set; }
        //public ICommand<IReadOnlyList<object>> ItemTappedCommand { get; }

        public ICommand LoadDatabase { get; }

        public ContactsListPageViewModel()
        {
            ContactsList = new ObservableCollection<ContactModel>();

            AddContactCommand = new Command(() => MainThread.BeginInvokeOnMainThread(async () => await Shell.Current.GoToAsync("contactlist/contactcreate").ConfigureAwait(false)));

            SettingsCommand = new Command(() => MainThread.BeginInvokeOnMainThread(async () => await Shell.Current.GoToAsync("contactlist/settings").ConfigureAwait(false)));

            //ItemTappedCommand = new Command<IReadOnlyList<object>>((o) => ShowDetails(o, dialogService));

            LoadDatabase = new Command(LoadData);
        }

        public async void ShowDetails(IReadOnlyList<object> obj)
        {
            if (obj is null)
            {
                return;
            }

            List<object> list = (List<object>)obj;
            ContactModel[] contact = list.ConvertAll(item => (ContactModel)item).ToArray();

            if (contact == null)
            {
                throw new ArgumentException("Given contact is null");
            }
            string Message = $"{contact[0].Name} {contact[0].Surname}\nPhone number: {contact[0].PhoneNumber}\nE-mail: {contact[0].Email}\nAddress: {contact[0].Address}";

            //await dialogService.DisplayAlertAsync("Details", Message, "Close").ConfigureAwait(false);
        }

        public async void LoadData()
        {
            ContactsList.Clear();
            List<ContactModel> database = await App.Database.GetContactsAsync().ConfigureAwait(false);
            foreach (ContactModel contact in database)
            {
                ContactsList.Add(contact);
            }

            IsLoading = false;
        }
    }
}