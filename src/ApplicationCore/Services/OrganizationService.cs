using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class OrganizationService : Service<Organization>, IOrganizationService
    {
        public OrganizationService(IRepository<Organization> repository)
            : base(repository) { }

        public async Task<IReadOnlyCollection<Country>> GetCountriesAsync(int id)
        {
            var entity = await _repository.GetByIdWithChildrenAsync(id, "Countries");

            return entity?.Countries as IReadOnlyCollection<Country>;
        }
    }
}
