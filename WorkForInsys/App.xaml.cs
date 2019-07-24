using Prism;
using Prism.Ioc;
using WorkForInsys.ViewModels;
using WorkForInsys.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace WorkForInsys
{
    public partial class App
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Routing.RegisterRoute("contactlist", typeof(ContactsListPage));
            Routing.RegisterRoute("contactlist/contactcreate", typeof(ContactCreatePage));
            Routing.RegisterRoute("contactlist/contactdetail", typeof(ContactDetailPage));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<AppShell>();
            containerRegistry.RegisterForNavigation<ContactsListPage, ContactsListPageViewModel>();
            containerRegistry.RegisterForNavigation<ContactDetailPage, ContactDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<ContactCreatePage, ContactCreatePageViewModel>();
        }
    }
}