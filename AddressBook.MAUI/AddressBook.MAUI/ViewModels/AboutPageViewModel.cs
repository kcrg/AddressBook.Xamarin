using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;
using System.Windows.Input;

namespace AddressBook.MAUI.ViewModels
{
    public class AboutPageViewModel : BindableObject
    {
        public ICommand SettingsCommand { get; }
        public ICommand PhoneTappedCommand { get; }
        public ICommand EmailTappedCommand { get; }
        public ICommand OpenGithubCommand { get; }

        public AboutPageViewModel()
        {
            SettingsCommand = new Command(() => MainThread.BeginInvokeOnMainThread(async () => await Shell.Current.GoToAsync("contactlist/settings").ConfigureAwait(false)));

            PhoneTappedCommand = new Command(() =>
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

            EmailTappedCommand = new Command(async () =>
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

            OpenGithubCommand = new Command(async () => await Browser.OpenAsync("https://github.com/kcrg", BrowserLaunchMode.SystemPreferred).ConfigureAwait(false));
        }

        private void ShowToastMessage(string message, int exceptionCode)
        {
            Console.WriteLine(message + "\nError code: " + exceptionCode);
            //UserDialogs.Instance.Toast(message + "\nError code: " + exceptionCode);
        }
    }
}