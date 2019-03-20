using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class CountryService : Service<Country>, ICountryService
    {
        public CountryService(IRepository<Country> repository)
            : base(repository) { }

        public async Task<IReadOnlyCollection<Business>> GetBusinessesAsync(int id)
        {
            var country = await _repository.GetByIdWithChildrenAsync(id, "Business");

            return country?.Businesses as IReadOnlyCollection<Business>;
        }
    }
}
