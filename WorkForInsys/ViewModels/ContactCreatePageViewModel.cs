using Prism.Commands;
using Prism.Mvvm;

namespace WorkForInsys.ViewModels
{
    // https://docs.microsoft.com/pl-pl/xamarin/xamarin-forms/enterprise-application-patterns/validation

    public class ContactCreatePageViewModel : BindableBase
    {
        public DelegateCommand SaveContactCommand { get; private set; }

        public ContactCreatePageViewModel()
        {
            SaveContactCommand = new DelegateCommand(async () =>
            {

            });
        }
    }
}