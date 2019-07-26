using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WorkForInsys.Models;
using WorkForInsys.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkForInsys.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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
            if ((BindingContext is ContactsListPageViewModel vm) && vm.LoadDatabase.CanExecute())
            {
                vm.LoadDatabase.Execute();
            }
        }

        private async void DeleteContact(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;

            int ID = int.Parse(btn.CommandParameter.ToString());
            List<ContactModel> list = await App.Database.GetContactsAsync();
            ContactModel contactToDelete = list.Find(x => x.ID == ID);
            await App.Database.DeleteContactAsync(contactToDelete);

            LoadDatabase();
        }

        private async void EditContact(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;

            int ID = int.Parse(btn.CommandParameter.ToString());
            List<ContactModel> list = await App.Database.GetContactsAsync();
            ContactModel contact = list.Find(x => x.ID == ID);

            string contactJson = JsonConvert.SerializeObject(contact);

            await Shell.Current.GoToAsync($"contactlist/contactcreate?entry={contactJson}");
        }
    }
}