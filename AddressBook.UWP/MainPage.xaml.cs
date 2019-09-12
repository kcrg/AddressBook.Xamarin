using Prism;
using Prism.Ioc;

namespace AddressBook.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            LoadApplication(new AddressBook.App(new UWPInitializer()));
        }

        public class UWPInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                // Register any platform specific implementations
            }
        }
    }
}