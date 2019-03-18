using ApplicationCore.ValueObjects;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities
{
    public class User : BaseEntity, IAggregateRoot
    {
        public int Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }
        
        public Address Address { get; private set; }

        // For EF core
        public User() { }

        public User(Address address)
        {
            this.Address = address;
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
