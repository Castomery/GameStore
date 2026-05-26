using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameVault.Application.Interfaces.Repositories;
using GameVault.Domain.Models;
using GameVault.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Infrastructure.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext appDbContext) : base(appDbContext) { }
       
        public async Task<List<Review>> GetByGameIdAsync(int gameId)
        {
           return await _appDbContext.Reviews.Where(r => r.GameId == gameId).ToListAsync();
        }

        public async Task<bool> ReviewExistsAsync(int userId, int gameId)
        {
            bool exists = await _appDbContext.Reviews.AnyAsync(r => r.UserId == userId && r.GameId == gameId);

            return exists;
        }
    }
}
