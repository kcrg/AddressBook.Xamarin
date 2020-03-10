using AddressBook.Services;
using Prism.Mvvm;

namespace AddressBook.ViewModels
{
    public class SettingsPageViewModel : BindableBase
    {
        public bool IsDarkThemeEnabled
        {
            get => _settingsService.SavedTheme == ApplicationTheme.Dark.ToString();
            set => _settingsService.AppTheme = value ? ApplicationTheme.Dark : ApplicationTheme.Light;
        }

        private ISettingsService _settingsService { get; }

        public SettingsPageViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;

            //IsDarkThemeEnabled = _settingsService.SavedTheme == ApplicationTheme.Dark.ToString();
        }
    }
}