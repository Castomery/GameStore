using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameVault.Application.Interfaces.Repositories;
using GameVault.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _appDbContext;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            var entry =  await _appDbContext.Set<T>().AddAsync(entity);

            await _appDbContext.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _appDbContext.Set<T>().FindAsync(id);

            if (entity == null) return false;

            _appDbContext.Set<T>().Remove(entity);
            await _appDbContext.SaveChangesAsync();

            return true;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            var entries = await _appDbContext.Set<T>().ToListAsync();
            return entries;
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            var entity = await _appDbContext.Set<T>().FindAsync(id);

            return entity;
        }

        public virtual async Task<T> UpdateAsync(int id, T entity)
        {
            var existing = await _appDbContext.Set<T>().FindAsync(id);

            if (existing == null) throw new KeyNotFoundException($"Entity with id {id} not found");

            _appDbContext.Entry(existing).CurrentValues.SetValues(entity);

            await _appDbContext.SaveChangesAsync();

            return existing;
        }
    }
}
