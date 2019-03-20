using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class OfferingService : Service<Offering>, IOfferingService
    {
        public OfferingService(IRepository<Offering> repository)
            : base(repository) { }

        public async Task<IReadOnlyCollection<Department>> GetDepartmentsAsync(int id)
        {
            var offering = await _repository.GetByIdWithChildrenAsync(id, "Departments");

            return offering?.Departments as IReadOnlyCollection<Department>;
        }
    }
}
