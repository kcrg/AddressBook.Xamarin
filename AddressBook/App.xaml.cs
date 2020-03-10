using AddressBook.Services;
using AddressBook.Services.Implementations;
using AddressBook.Styles;
using AddressBook.ViewModels;
using AddressBook.Views;
using Prism;
using Prism.Ioc;
using System;
using System.IO;
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

        private static ContactsDatabaseService databaseValue;

        public static ContactsDatabaseService Database =>
            databaseValue ?? (databaseValue = new ContactsDatabaseService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ContactsDatabase.db")));

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

            return width <= smallWightResolution || height <= smallHeightResolution;
        }

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            LoadStyles();

            MainPage = new AppShell();

            Routing.RegisterRoute("about", typeof(AboutPage));
            Routing.RegisterRoute("about/settings", typeof(SettingsPage));

            Routing.RegisterRoute("contactlist", typeof(ContactsListPage));
            Routing.RegisterRoute("contactlist/contactcreate", typeof(ContactCreatePage));
            Routing.RegisterRoute("contactlist/settings", typeof(SettingsPage));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register(typeof(ISettingsService), typeof(SettingsService));

            containerRegistry.RegisterForNavigation<AppShell>();
            containerRegistry.RegisterForNavigation<ContactsListPage, ContactsListPageViewModel>();
            containerRegistry.RegisterForNavigation<ContactCreatePage, ContactCreatePageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
        }
    }
}