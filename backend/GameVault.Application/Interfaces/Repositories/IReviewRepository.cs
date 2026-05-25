using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameVault.Domain.Models;

namespace GameVault.Application.Interfaces.Repositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<List<Review>> GetByGameIdAsync(int gameId);
        Task<bool> ReviewExistsAsync(int userId, int gameId);
    }
}
