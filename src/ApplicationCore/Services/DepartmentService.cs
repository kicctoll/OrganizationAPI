using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class DepartmentService : Service<Department>, IDepartmentService
    {
        public DepartmentService(IRepository<Department> repository)
            : base(repository) { }
    }
}
