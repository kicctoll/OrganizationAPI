using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class Service<T> : IService<T> where T : BaseEntity
    {
        protected readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual async Task<T> GetItemByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            return await _repository.AddAsync(entity);
        }

        public virtual async Task UpdateAsync(int id, T newEntity)
        {
            var oldEntity = await _repository.GetByIdAsync(id);

            if (oldEntity != null)
            {
                await _repository.UpdateAsync(oldEntity, newEntity);
            }
            else
            {
                throw new Exception($"Entity type {typeof(T).Name} with id {id} doesn't exist!");
            }
        }

        public virtual async Task DeleteAsync(int id)
        {
            var requiredEntity = await _repository.GetByIdAsync(id);

            if (requiredEntity != null)
            {
                await _repository.DeleteAsync(requiredEntity);
            }
            else
            {
                throw new Exception($"Entity type {typeof(T).Name} with id {id} doesn't exist!");
            }
        }
    }
}
