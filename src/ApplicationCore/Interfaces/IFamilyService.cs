using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IFamilyService : IService<Family>
    {
        Task<IReadOnlyCollection<Offering>> GetOfferingsAsync(int id);
    }
}
