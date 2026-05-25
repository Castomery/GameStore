using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameVault.Domain.Models;
using GameVault.Application.Interfaces.Repositories;
using GameVault.Infrastructure.Data;

namespace GameVault.Infrastructure.Repositories
{
    public class GameRepository : Repository<Game>, IGameRepository
    {

        public GameRepository(AppDbContext appDbContext) : base(appDbContext) { }
        public Task<List<Game>> GetByGenreAsync(int genreId)
        {
            throw new NotImplementedException();
        }

        public Task<Game?> GetWithReviewsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Game>> SearchByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }
    }
}
