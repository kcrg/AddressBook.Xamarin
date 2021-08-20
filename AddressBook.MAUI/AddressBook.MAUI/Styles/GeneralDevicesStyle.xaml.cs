using Microsoft.Maui.Controls;

namespace AddressBook.MAUI.Styles
{
    public partial class GeneralDevicesStyle : ResourceDictionary
    {
        public static GeneralDevicesStyle SharedInstance { get; } = new GeneralDevicesStyle();

        public GeneralDevicesStyle()
        {
            InitializeComponent();
        }
    }
}