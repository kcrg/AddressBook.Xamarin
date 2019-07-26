using Newtonsoft.Json;
using System;
using WorkForInsys.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkForInsys.Views
{
    [QueryProperty("Entry", "entry")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactCreatePage : ContentPage
    {
        private string Json;
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

                IDLabel.Text = $"You are changing contact \nwith ID {contactToEdit.ID.ToString()}.";
                NameEntry.Text = contactToEdit.Name;
                SurnameEntry.Text = contactToEdit.Surname;
                PhoneEntry.Text = contactToEdit.PhoneNumber;
                EmailEntry.Text = contactToEdit.Email;
                AddressEntry.Text = contactToEdit.Address;
            }
            else
            {
                Title = "Add";
                IDLabel.Text = "You are adding a new contact.";
            }
        }
    }
}