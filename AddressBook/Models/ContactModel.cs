using SQLite;

namespace AddressBook.Models
{
    public class ContactModel
    {
        [PrimaryKey, AutoIncrement, Indexed]
        public int ID { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}