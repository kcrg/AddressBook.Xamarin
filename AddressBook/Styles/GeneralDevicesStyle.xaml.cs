﻿using Xamarin.Forms;

namespace AddressBook.Styles
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