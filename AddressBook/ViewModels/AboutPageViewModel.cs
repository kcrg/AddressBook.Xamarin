using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AddressBook.ViewModels
{
    public class AboutPageViewModel : BindableBase
    {
        public DelegateCommand SettingsCommand { get; }
        public DelegateCommand PhoneTappedCommand { get; }
        public DelegateCommand EmailTappedCommand { get; }
        public DelegateCommand OpenGithubCommand { get; }

        public AboutPageViewModel()
        {
            SettingsCommand = new DelegateCommand(() => MainThread.BeginInvokeOnMainThread(async () => await Shell.Current.GoToAsync("contactlist/settings").ConfigureAwait(false)));

            PhoneTappedCommand = new DelegateCommand(() =>
            {
                try
                {
                    PhoneDialer.Open("+48733428869");
                }
                catch (ArgumentNullException ex)
                {
                    ShowToastMessage("The phone number is incorrect.", ex.HResult);
                }
                catch (FeatureNotSupportedException ex)
                {
                    ShowToastMessage("Device does not support phone calls or does not have phone app.", ex.HResult);
                }
                catch (Exception ex)
                {
                    ShowToastMessage("An error occurred while opening the phone application.", ex.HResult);
                }
            });

            EmailTappedCommand = new DelegateCommand(async () =>
            {
                try
                {
                    EmailMessage message = new EmailMessage
                    {
                        To = { "kacper@tryniecki.com" },
                    };
                    await Email.ComposeAsync(message).ConfigureAwait(false);
                }
                catch (FeatureNotSupportedException ex)
                {
                    ShowToastMessage("Device does not support E-mail or does not have E-mail app.", ex.HResult);
                }
                catch (Exception ex)
                {
                    ShowToastMessage("An error occurred while opening the email application.", ex.HResult);
                }
            });

            OpenGithubCommand = new DelegateCommand(async () => await Browser.OpenAsync("https://github.com/kcrg", BrowserLaunchMode.SystemPreferred).ConfigureAwait(false));
        }

        private void ShowToastMessage(string message, int exceptionCode)
        {
            UserDialogs.Instance.Toast(message + "\nError code: " + exceptionCode);
        }
    }
}