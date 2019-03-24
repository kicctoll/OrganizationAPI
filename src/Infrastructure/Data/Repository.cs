using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        protected readonly DbSet<T> _entities;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<T> GetByIdWithChildrenAsync(int id, string childrenName)
        {
            return await _entities.Include(childrenName).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> AddAsync(T entity)
        {
            _entities.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T oldEntity, T newEntity)
        {
            _context.Entry(oldEntity)
                .CurrentValues
                .SetValues(newEntity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _entities.Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}
