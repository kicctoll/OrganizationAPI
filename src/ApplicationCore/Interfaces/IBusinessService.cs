using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IBusinessService : IService<Business>
    {
        Task<IReadOnlyCollection<Family>> GetFamiliesAsync(int id);
    }
}
