using AddressBook.MAUI.Services.Implementations;
using AddressBook.MAUI.Styles;
using AddressBook.MAUI.Views;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using Microsoft.Maui.Essentials;
using Application = Microsoft.Maui.Controls.Application;

namespace AddressBook.MAUI
{
    public partial class App : Application
    {
        private const int smallWightResolution = 768;
        private const int smallHeightResolution = 1280;

        private static ContactsDatabaseService? databaseValue;

        public static ContactsDatabaseService Database => databaseValue ??=
            new ContactsDatabaseService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ContactsDatabase.db"));

        public App()
        {
            InitializeComponent();
            LoadStyles();

            MainPage = new AppShell();
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

            return width <= smallWightResolution || height <= smallHeightResolution;
        }
    }
}
