using System.Threading.Tasks;
using System.Collections.Generic;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IOrganizationService : IService<Organization>
    {
        Task<IReadOnlyCollection<Country>> GetCountriesAsync(int id);
    }
}
