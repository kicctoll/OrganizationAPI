using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Business : BaseEntityChild<Country>
    {
        public string Name { get; private set; }

        public IEnumerable<Family> Families { get; private set; }

        // For EF Core
        public Business() { }

        public Business(string name)
        {
            this.Name = name;
        }
    }
}
