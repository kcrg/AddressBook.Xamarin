using AddressBook.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AddressBook.ViewModels
{
    public class ContactsListPageViewModel : BindableBase
    {
        private bool _isLoading = true;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public ObservableCollection<ContactModel> ContactsList { get; }
        public DelegateCommand AddContactCommand { get; }
        public DelegateCommand SettingsCommand { get; }
        public DelegateCommand<string> DeleteContactCommand { get; set; }
        public DelegateCommand<IReadOnlyList<object>> ItemTappedCommand { get; }

        public DelegateCommand LoadDatabase { get; }

        public ContactsListPageViewModel(IPageDialogService dialogService)
        {
            ContactsList = new ObservableCollection<ContactModel>();

            AddContactCommand = new DelegateCommand(() => MainThread.BeginInvokeOnMainThread(async () => await Shell.Current.GoToAsync("contactlist/contactcreate").ConfigureAwait(false)));

            SettingsCommand = new DelegateCommand(() => MainThread.BeginInvokeOnMainThread(async () => await Shell.Current.GoToAsync("contactlist/settings").ConfigureAwait(false)));

            ItemTappedCommand = new DelegateCommand<IReadOnlyList<object>>((o) => ShowDetails(o, dialogService));

            LoadDatabase = new DelegateCommand(LoadData);
        }

        public async void ShowDetails(IReadOnlyList<object> obj, IPageDialogService dialogService)
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

            await dialogService.DisplayAlertAsync("Details", Message, "Close").ConfigureAwait(false);
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