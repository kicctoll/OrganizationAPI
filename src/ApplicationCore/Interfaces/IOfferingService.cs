using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IOfferingService : IService<Offering>
    {
        Task<IReadOnlyCollection<Department>> GetDepartmentsAsync(int id);
    }
}
