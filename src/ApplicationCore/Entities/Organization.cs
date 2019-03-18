using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class Organization : BaseEntity
    {
        public string Name { get; private set; }
        
        public long Code { get; private set; }

        public string OrganizationType { get; private set; }

        public string Owner { get; private set; }

        public IEnumerable<Country> Countries { get; private set; }

        public Organization() { }

        public Organization(string name, long code, string organizationType, string owner)
        {
            this.Name = name;
            this.Code = code;
            this.OrganizationType = organizationType;
            this.Owner = owner;
        }
    }
}
