namespace ApplicationCore.ValueObjects
{
    public class Address
    {
        public string Country { get; private set; }

        public string City { get; private set; }

        public Address(string country, string city)
        {
            this.Country = country;
            this.City = city;
        }
    }
}
