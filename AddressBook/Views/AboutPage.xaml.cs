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
            await Task.Delay(200);

            _ = await Task.WhenAll(
            ProfilePicture.TranslateTo(0, -60, 1100, Easing.CubicOut),
            ProfilePicture.FadeTo(100, 1400, Easing.CubicIn)
            );

            _ = await Task.WhenAll(
               DescriptionPanel.TranslateTo(0, -15, 1000, Easing.CubicOut),
               DescriptionPanel.FadeTo(100, 1000, Easing.CubicIn),
               GithubButton.FadeTo(100, 1000, Easing.CubicIn)
               );
        }
    }
}