using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WorkForInsys.Models;
using Xamarin.Forms;

namespace WorkForInsys.ViewModels
{
    public class ContactsListPageViewModel : BindableBase
    {
        private bool _isLoading = true;
        private bool _isBlocked;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public bool IsBlocked
        {
            get => _isBlocked;
            set => SetProperty(ref _isBlocked, value);
        }

        public ObservableCollection<ContactModel> ContactsList { get; private set; }
        public DelegateCommand AddContactCommand { get; private set; }
        public DelegateCommand<string> DeleteContactCommand { get; set; }
        public DelegateCommand<IReadOnlyList<object>> ItemTappedCommand { get; private set; }

        public DelegateCommand LoadDatabase { get; private set; }

        public ContactsListPageViewModel()
        {
            ContactsList = new ObservableCollection<ContactModel>();

            AddContactCommand = new DelegateCommand(async () =>
            {
                await Shell.Current.GoToAsync("contactlist/contactcreate");
            });

            ItemTappedCommand = new DelegateCommand<IReadOnlyList<object>>(ShowDetails, (o) => !IsBlocked);

            LoadDatabase = new DelegateCommand(LoadData);
        }

        public void ShowDetails(IReadOnlyList<object> obj)
        {
            IsBlocked = true;

            List<object> list = (List<object>)obj;
            ContactModel[] contact = list.ConvertAll(item => (ContactModel)item).ToArray();

            if (contact == null)
            {
                throw new ArgumentException("Given contact is null");
            }
            string Message = $"{contact[0].Name} {contact[0].Surname}\nPhone number: {contact[0].PhoneNumber}\nE-mail: {contact[0].Email}\nAddress: {contact[0].Address}";

            IsBlocked = false;
            UserDialogs.Instance.AlertAsync(Message, "Details", "Close");
        }

        public async void LoadData()
        {
            ContactsList.Clear();
            List<ContactModel> database = await App.Database.GetContactsAsync();
            foreach (ContactModel contact in database)
            {
                ContactsList.Add(contact);
            }

            IsLoading = false;
        }
    }
}