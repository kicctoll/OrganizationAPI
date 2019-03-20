using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface ICountryService : IService<Country>
    {
        Task<IReadOnlyCollection<Business>> GetBusinessesAsync(int id);
    }
}
