using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameVault.Application.Interfaces.Repositories;
using GameVault.Domain.Models;
using GameVault.Infrastructure.Data;

namespace GameVault.Infrastructure.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(AppDbContext appDbContext) : base(appDbContext) { }
        
    }
}
