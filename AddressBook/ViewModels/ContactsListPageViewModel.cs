using AddressBook.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<ContactModel> ContactsList { get; private set; }
        public DelegateCommand AddContactCommand { get; private set; }
        public DelegateCommand<string> DeleteContactCommand { get; set; }
        public DelegateCommand<IReadOnlyList<object>> ItemTappedCommand { get; private set; }

        public DelegateCommand LoadDatabase { get; private set; }

        public ContactsListPageViewModel(IPageDialogService dialogService)
        {
            ContactsList = new ObservableCollection<ContactModel>();

            AddContactCommand = new DelegateCommand(async () =>
            {
                await Shell.Current.GoToAsync("contactlist/contactcreate");
            });

            ItemTappedCommand = new DelegateCommand<IReadOnlyList<object>>((o) =>
            {
                ShowDetails(o, dialogService);
            });

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

            await dialogService.DisplayAlertAsync("Details", Message, "Close");
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