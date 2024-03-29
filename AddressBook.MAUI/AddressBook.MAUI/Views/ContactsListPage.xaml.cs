﻿using AddressBook.MAUI.Models;
using AddressBook.MAUI.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;
using Newtonsoft.Json;
using System;

namespace AddressBook.MAUI.Views
{
    public partial class ContactsListPage : ContentPage
    {
        public ContactsListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LoadDatabase();
        }

        private void LoadDatabase()
        {
            //if ((BindingContext is ContactsListPageViewModel vm) && vm.LoadDatabase.CanExecute())
            //{
            //    vm.LoadDatabase.Execute();
            //}
        }

        private async void DeleteContact(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;

            int ID = int.Parse(btn.CommandParameter.ToString());
            ContactModel contactToDelete = await App.Database.GetContactAsync(ID).ConfigureAwait(false);

            await App.Database.DeleteContactAsync(contactToDelete).ConfigureAwait(false);

            LoadDatabase();
        }

        private async void EditContact(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;

            int ID = int.Parse(btn.CommandParameter.ToString());
            ContactModel contactToEdit = await App.Database.GetContactAsync(ID).ConfigureAwait(false);

            string contactJson = JsonConvert.SerializeObject(contactToEdit);
            MainThread.BeginInvokeOnMainThread(async () => await Shell.Current.GoToAsync($"contactlist/contactcreate?entry={contactJson}").ConfigureAwait(false));
        }
    }
}