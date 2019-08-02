using Prism;
using Prism.Ioc;
using System;
using System.IO;
using AddressBook.Services;
using AddressBook.Styles;
using AddressBook.ViewModels;
using AddressBook.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AddressBook
{
    public partial class App
    {
        private const int smallWightResolution = 768;
        private const int smallHeightResolution = 1280;

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

        private void LoadStyles()
        {
            if (IsASmallDevice())
            {
                dictionary.MergedDictionaries.Add(SmallDevicesStyle.SharedInstance);
            }
            else
            {
                dictionary.MergedDictionaries.Add(GeneralDevicesStyle.SharedInstance);
            }
        }

        public static bool IsASmallDevice()
        {
            // Get Metrics
            DisplayInfo mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Get Resolution 
            double width = mainDisplayInfo.Width;
            double height = mainDisplayInfo.Height;

            return (width <= smallWightResolution && height <= smallHeightResolution);
        }

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            LoadStyles();

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