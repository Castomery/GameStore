using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameVault.Application.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(int id, T entity);
        Task<bool> DeleteAsync(int id);
    }
}
