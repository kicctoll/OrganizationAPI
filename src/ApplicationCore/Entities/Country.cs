using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class Country : BaseEntityChild<Organization>
    {
        public string Name { get; private set; }

        public long Code { get; private set; }

        public IEnumerable<Business> Businesses { get; private set; }

        // For EF Core
        public Country() { }

        public Country(string name, long code)
        {
            this.Name = name;
            this.Code = code;
        }
    }
}
