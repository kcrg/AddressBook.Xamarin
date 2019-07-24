using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using System;
using Xamarin.Essentials;

namespace WorkForInsys.ViewModels
{
    public class AboutPageViewModel : BindableBase
    {
        public DelegateCommand PhoneTappedCommand { get; private set; }
        public DelegateCommand EmailTappedCommand { get; private set; }
        public DelegateCommand OpenGithubCommand { get; private set; }

        public AboutPageViewModel()
        {
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
                    await Email.ComposeAsync(message);
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

            OpenGithubCommand = new DelegateCommand(async () =>
            {
                await Browser.OpenAsync("https://github.com/kcrg", BrowserLaunchMode.SystemPreferred);
            });
        }

        private void ShowToastMessage(string message, int exceptionCode)
        {
            UserDialogs.Instance.Toast(message + "\nError code: " + exceptionCode);
        }
    }
}