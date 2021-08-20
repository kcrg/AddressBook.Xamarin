using AddressBook.MAUI.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace AddressBook.MAUI.ViewModels
{
    public class ContactCreatePageViewModel : BindableObject
    {
        public string? _validateMessage;

        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

        public string? ValidateMessage
        {
            get;
            set;
        }

        public ICommand SaveContactCommand { get; }

        public ContactCreatePageViewModel()
        {
            SaveContactCommand = new Command(AddContact);
        }

        public async void AddContact()
        {
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Surname) ||
                string.IsNullOrWhiteSpace(PhoneNumber) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Address) ||
                !IsEmail(Email) ||
                !IsNumber(PhoneNumber))
            {
                ValidateMessage = "Please complete all fields correctly.";
            }
            else
            {
                ContactModel contact = new()
                {
                    ID = ID,
                    Name = Name,
                    Surname = Surname,
                    PhoneNumber = PhoneNumber,
                    Email = Email,
                    Address = Address
                };

                await App.Database.SaveContactAsync(contact).ConfigureAwait(false);

                MainThread.BeginInvokeOnMainThread(async () => await Shell.Current.Navigation.PopAsync().ConfigureAwait(false));
            }
        }

        public bool IsEmail(string value)
        {
            if (value is null)
            {
                return false;
            }

            Regex regex = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(value);

            return match.Success;
        }

        public bool IsNumber(string value)
        {
            if (value is null || value?.Length != 9)
            {
                return false;
            }

            Regex regex = new("^[0-9]+$");
            Match match = regex.Match(value);

            return match.Success;
        }
    }
}