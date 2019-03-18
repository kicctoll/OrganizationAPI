using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Offering : BaseEntityChild<Family>
    {
        public string Name { get; private set; }

        public IEnumerable<Department> Departments { get; private set; }

        // For EF Core
        public Offering() { }

        public Offering(string name)
        {
            this.Name = name;
        }
    }
}
