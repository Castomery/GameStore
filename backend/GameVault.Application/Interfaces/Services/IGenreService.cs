using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameVault.Application.Dtos.Genre;

namespace GameVault.Application.Interfaces.Services
{
    public interface IGenreService
    {
        Task<List<GenreDto>> GetAllGenresAsync();
        Task<GenreDto> CreateGenreAsync(CreateGenreDto createGenreDto);
        Task<bool> DeleteGenreAsync(int id);
    }
}
