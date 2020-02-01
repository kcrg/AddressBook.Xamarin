using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AddressBook.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            MainThread.BeginInvokeOnMainThread(StartAnimation);
        }

        private async void StartAnimation()
        {
            // Waiting for the page to load
            await Task.Delay(200).ConfigureAwait(false);

            await Task.WhenAll(
               DescriptionPanel.TranslateTo(0, -15, 1000, Easing.CubicOut),
               DescriptionPanel.FadeTo(100, 1000, Easing.CubicIn),
               GithubButton.FadeTo(100, 1000, Easing.CubicIn)
               ).ConfigureAwait(false);
        }
    }
}