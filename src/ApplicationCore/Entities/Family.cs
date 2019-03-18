using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Family : BaseEntityChild<Business>
    {
        public string Name { get; private set; }

        public IEnumerable<Offering> Offerings { get; private set; }

        // For EF Core
        public Family() { }

        public Family(string name)
        {
            this.Name = name;
        }
    }
}
