using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameVault.Domain.Models;
using GameVault.Application.Interfaces.Repositories;
using GameVault.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Infrastructure.Repositories
{
    public class GameRepository : Repository<Game>, IGameRepository
    {

        public GameRepository(AppDbContext appDbContext) : base(appDbContext) { }
        public async Task<List<Game>> GetByGenreAsync(int genreId)
        {
            List<Game> games = await _appDbContext.Games.Where(g => g.GenreId == genreId).ToListAsync();
            return games;
        }

        public async Task<Game?> GetWithReviewsAsync(int id)
        {
            return await _appDbContext.Games.Include(g => g.Reviews).FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<List<Game>> SearchByTitleAsync(string title)
        {
            return await _appDbContext.Games.Where(g => g.Title.Contains(title)).ToListAsync();
        }
    }
}
