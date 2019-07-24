using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkForInsys.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsListPage : ContentPage
    {
        public ContactsListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //collectionView.ItemsSource = await App.Database.GetContactsAsync();
        }
    }
}