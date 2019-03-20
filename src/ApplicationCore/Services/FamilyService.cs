using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class FamilyService : Service<Family>, IFamilyService
    {
        public FamilyService(IRepository<Family> repository)
            : base(repository) { }

        public async Task<IReadOnlyCollection<Offering>> GetOfferingsAsync(int id)
        {
            var family = await _repository.GetByIdWithChildrenAsync(id, "Offerings");

            return family?.Offerings as IReadOnlyCollection<Offering>;
        }
    }
}
