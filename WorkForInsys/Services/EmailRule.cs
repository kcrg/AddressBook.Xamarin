using System.Text.RegularExpressions;
using WorkForInsys.Interfaces;

namespace WorkForInsys.Services
{
    public class EmailRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            string str = value as string;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(str);

            return match.Success;
        }
    }
}