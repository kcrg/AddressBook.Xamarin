using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkForInsys.Views
{
    [QueryProperty("Entry", "entry")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactDetailPage : ContentPage
    {
        public ContactDetailPage()
        {
            InitializeComponent();
        }

        public string Entry
        {
            set => detail.Text = Uri.UnescapeDataString(value).ToString();
        }
    }
}