using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameVault.Domain.Models;

namespace GameVault.Application.Interfaces.Repositories
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<List<Game>> GetByGenreAsync(int genreId);
        Task<List<Game>> SearchByTitleAsync(string title);
        Task<Game?> GetWithReviewsAsync(int id);

    }
}
