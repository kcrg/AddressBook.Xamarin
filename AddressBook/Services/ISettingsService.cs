namespace AddressBook.Services
{
    public interface ISettingsService
    {
        string SavedTheme { get; set; }
        ApplicationTheme AppTheme { get; set; }
    }

    public enum ApplicationTheme
    {
        Default = -1,
        Auto,
        Light,
        Dark
    }
}