using ApplicationCore.ValueObjects;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities
{
    public class User : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string Email { get; private set; }
        
        public Address Address { get; private set; }

        // For EF core
        public User() { }

        public User(string name, string surname, string email, Address address)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Address = address;
        }

        public void ChangeAddress(Address newAddress)
        {
            if (!string.IsNullOrWhiteSpace(newAddress.City) && !string.IsNullOrWhiteSpace(newAddress.Country))
            {
                this.Address = newAddress;
            }
        }
    }
}
