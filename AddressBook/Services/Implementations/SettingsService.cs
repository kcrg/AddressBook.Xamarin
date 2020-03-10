using Xamarin.Essentials;

namespace AddressBook.Services.Implementations
{
    public class SettingsService : ISettingsService
    {
        public SettingsService()
        {
            LoadTheme();
        }

        public string SavedTheme
        {
            get => Preferences.Get("AppTheme", nameof(ApplicationTheme.Default));
            set => Preferences.Set("AppTheme", value);
        }

        public ApplicationTheme AppTheme
        {
            get
            {
                if (SavedTheme == nameof(ApplicationTheme.Light))
                {
                    return ApplicationTheme.Light;
                }
                else if (SavedTheme == nameof(ApplicationTheme.Dark))
                {
                    return ApplicationTheme.Dark;
                }
                else
                {
                    return ApplicationTheme.Light;
                }
            }
            set => SetAppTheme();
        }

        protected ApplicationTheme SetAppTheme()
        {
            return AppTheme switch
            {
                ApplicationTheme.Dark => SetDarkTheme(),
                ApplicationTheme.Light => SetLightTheme(),
                _ => SetLightTheme(),
            };
        }

        private ApplicationTheme SetLightTheme()
        {
            //Application.Current.Resources["Page"] = Application.Current.Resources["LightPage"];

            SavedTheme = nameof(ApplicationTheme.Light);
            return ApplicationTheme.Light;
        }

        private ApplicationTheme SetDarkTheme()
        {
            //Application.Current.Resources["Page"] = Application.Current.Resources["DarkPage"];
            SavedTheme = nameof(ApplicationTheme.Dark);
            return ApplicationTheme.Dark;
        }

        private void LoadTheme()
        {
            if (SavedTheme == nameof(ApplicationTheme.Light))
            {
                AppTheme = ApplicationTheme.Light;
            }
            else if (SavedTheme == nameof(ApplicationTheme.Dark))
            {
                AppTheme = ApplicationTheme.Dark;
            }
            else
            {
                AppTheme = ApplicationTheme.Light;
            }
        }
    }
}
