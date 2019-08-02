using Newtonsoft.Json;
using System;
using AddressBook.Models;
using AddressBook.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AddressBook.Views
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
            ContactModel contactToDelete = await App.Database.GetContactAsync(ID);

            await App.Database.DeleteContactAsync(contactToDelete);

            LoadDatabase();
        }

        private async void EditContact(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;

            int ID = int.Parse(btn.CommandParameter.ToString());
            ContactModel contactToEdit = await App.Database.GetContactAsync(ID);

            string contactJson = JsonConvert.SerializeObject(contactToEdit);

            await Shell.Current.GoToAsync($"contactlist/contactcreate?entry={contactJson}");
        }
    }
}