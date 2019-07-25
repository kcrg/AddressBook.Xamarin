using WorkForInsys.ViewModels;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if ((BindingContext is ContactsListPageViewModel vm) && vm.LoadDatabase.CanExecute())
            {
                vm.LoadDatabase.Execute();
            }
        }
    }
}