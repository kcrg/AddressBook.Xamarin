﻿using AddressBook.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Text.RegularExpressions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AddressBook.ViewModels
{
    public class ContactCreatePageViewModel : BindableBase
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
            get => _validateMessage;
            set => SetProperty(ref _validateMessage, value);
        }

        public DelegateCommand SaveContactCommand { get; }

        public ContactCreatePageViewModel()
        {
            SaveContactCommand = new DelegateCommand(AddContact);
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
                ContactModel contact = new ContactModel()
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

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
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