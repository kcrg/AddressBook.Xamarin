using Prism.Commands;
using Prism.Mvvm;
using System.Text.RegularExpressions;
using WorkForInsys.Models;
using Xamarin.Forms;

namespace WorkForInsys.ViewModels
{
    public class ContactCreatePageViewModel : BindableBase
    {
        public string _validateMessage;
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public string ValidateMessage
        {
            get => _validateMessage;
            set => SetProperty(ref _validateMessage, value);
        }

        public DelegateCommand SaveContactCommand { get; private set; }

        public ContactCreatePageViewModel()
        {
            SaveContactCommand = new DelegateCommand(() => AddContact());
        }

        public async void AddContact()
        {
            if (!IsEmpty(Name) || !IsEmpty(Surname) || !IsEmpty(PhoneNumber) || !IsEmpty(Email) || !IsEmpty(Address) || !IsEmail(Email) || !IsNumber(PhoneNumber))
            {
                ValidateMessage = "Please complete all fields correctly.";
            }
            else
            {
                ContactModel contact = new ContactModel()
                {
                    Name = Name,
                    Surname = Surname,
                    PhoneNumber = PhoneNumber,
                    Email = Email,
                    Address = Address
                };
                await App.Database.SaveContactAsync(contact);

                await Shell.Current.Navigation.PopAsync();
            }
        }

        public bool IsEmpty(string value)
        {
            if (value == null)
            {
                return false;
            }

            string str = value as string;
            return !string.IsNullOrWhiteSpace(str);
        }

        public bool IsEmail(string value)
        {
            if (value == null)
            {
                return false;
            }

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(value);

            return match.Success;
        }

        public bool IsNumber(string value)
        {
            if (value == null)
            {
                return false;
            }

            Regex regex = new Regex(@"^\d$");
            Match match = regex.Match(value);

            return match.Success;
        }
    }
}