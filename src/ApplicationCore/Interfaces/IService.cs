using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IService<T> where T : BaseEntity
    {
        Task<IReadOnlyCollection<T>> GetAllAsync();

        Task<T> GetItemByIdAsync(int id);

        Task<T> CreateAsync(T entity);

        Task UpdateAsync(int id, T newEntity);

        Task DeleteAsync(int id);
    }
}
