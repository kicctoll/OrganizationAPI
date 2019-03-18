using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class Department : BaseEntityChild<Offering>
    {
        public string Name { get; private set; }

        // For EF Core
        public Department() { }
        
        public Department(string name)
        {
            this.Name = name;
        }
    }
}
