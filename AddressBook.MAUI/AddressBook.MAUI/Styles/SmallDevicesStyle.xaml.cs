using Microsoft.Maui.Controls;

namespace AddressBook.MAUI.Styles
{
    public partial class SmallDevicesStyle : ResourceDictionary
    {
        public static SmallDevicesStyle SharedInstance { get; } = new SmallDevicesStyle();

        public SmallDevicesStyle()
        {
            InitializeComponent();
        }
    }
}