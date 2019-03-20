using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class BusinessService : Service<Business>, IBusinessService
    {
        public BusinessService(IRepository<Business> repository)
            : base(repository) { }

        public async Task<IReadOnlyCollection<Family>> GetFamiliesAsync(int id)
        {
            var business = await _repository.GetByIdWithChildrenAsync(id, "Families");

            return business?.Families as IReadOnlyCollection<Family>;
        }
    }
}
