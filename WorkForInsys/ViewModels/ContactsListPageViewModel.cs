using Newtonsoft.Json;
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
        public DelegateCommand<IReadOnlyList<object>> ItemTappedCommand { get; private set; }

        public ContactsListPageViewModel()
        {
            ContactsList = new ObservableCollection<ContactModel>();

            LoadData();

            AddContactCommand = new DelegateCommand(async () =>
            {
                await Shell.Current.GoToAsync("contactlist/contactcreate");
            });

            ItemTappedCommand = new DelegateCommand<IReadOnlyList<object>>(NavigateBlock, (o) => !IsBlocked);
        }

        public void NavigateBlock(IReadOnlyList<object> obj)
        {
            IsBlocked = true;

            List<object> list = (List<object>)obj;
            ContactModel[] contact = list.ConvertAll(item => (ContactModel)item).ToArray();

            NavigateToDetail(contact[0]);
        }

        public async void NavigateToDetail(ContactModel Contact)
        {
            if (Contact == null)
            {
                throw new ArgumentException("Given contact is null");
            }

            string entry = JsonConvert.SerializeObject(Contact);
            IsBlocked = false;
            await Shell.Current.GoToAsync($"contactlist/contactdetail?entry={entry}");
        }

        public async void LoadData()
        {
            List<ContactModel> lol = await App.Database.GetContactsAsync();
            foreach (ContactModel contact in lol)
            {
                ContactsList.Add(contact);
            }
            //ContactsList = await App.Database.GetContactsAsync();
            //Contacts = (List<ContactModel>)await App.Database.GetContactsAsync();
            //Contacts = new ObservableCollection<ContactModel>(ContactsList as List<ContactModel>);

            IsLoading = false;
        }
    }
}