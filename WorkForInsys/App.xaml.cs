using Prism;
using Prism.Ioc;
using System;
using System.IO;
using WorkForInsys.Data;
using WorkForInsys.ViewModels;
using WorkForInsys.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace WorkForInsys
{
    public partial class App
    {
        private static ContactsDatabase database;

        public static ContactsDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ContactsDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ContactsDatabase.db3"));
                }
                return database;
            }
        }

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Routing.RegisterRoute("contactlist", typeof(ContactsListPage));
            Routing.RegisterRoute("contactlist/contactcreate", typeof(ContactCreatePage));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<AppShell>();
            containerRegistry.RegisterForNavigation<ContactsListPage, ContactsListPageViewModel>();
            containerRegistry.RegisterForNavigation<ContactCreatePage, ContactCreatePageViewModel>();
        }
    }
}