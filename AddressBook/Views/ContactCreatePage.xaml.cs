using AddressBook.Models;
using Newtonsoft.Json;
using System;
using Xamarin.Forms;

namespace AddressBook.Views
{
    [QueryProperty("Entry", "entry")]
    public partial class ContactCreatePage : ContentPage
    {
        private string? Json;

        public string Entry
        {
            set => Json = Uri.UnescapeDataString(value);
        }

        public ContactCreatePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Json != null)
            {
                Title = "Edit";
                ContactModel contactToEdit = JsonConvert.DeserializeObject<ContactModel>(Json);

                IDLabel.Text = contactToEdit.ID.ToString();
                NameEntry.Text = contactToEdit.Name;
                SurnameEntry.Text = contactToEdit.Surname;
                PhoneEntry.Text = contactToEdit.PhoneNumber;
                EmailEntry.Text = contactToEdit.Email;
                AddressEntry.Text = contactToEdit.Address;
            }
            else
            {
                Title = "Add";
                welcomeLabel.Text = "You are adding a new contact.";
            }
        }
    }
}