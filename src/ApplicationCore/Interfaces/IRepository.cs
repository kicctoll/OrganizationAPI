using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IRepository<T> where T : BaseEntity 
    {
        Task<IReadOnlyCollection<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task<T> GetByIdWithChildrenAsync(int id, string childrenName);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T oldEntity, T newEntity);

        Task DeleteAsync(T Entity);
    }
}
