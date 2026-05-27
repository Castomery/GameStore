using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameVault.Application.Dtos.Game;
using GameVault.Application.Dtos.Genre;
using GameVault.Application.Interfaces.Repositories;
using GameVault.Application.Interfaces.Services;
using GameVault.Domain.Models;

namespace GameVault.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<GenreDto> CreateGenreAsync(CreateGenreDto createGenreDto)
        {
            var genre = new Genre
            {
                Name = createGenreDto.Name,
            };

            var createGenre = await _genreRepository.AddAsync(genre);

            return MapToDto(createGenre);
        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            return await _genreRepository.DeleteAsync(id);
        }

        public async Task<List<GenreDto>> GetAllGenresAsync()
        {
            var genres = await _genreRepository.GetAllAsync();
            return genres.Select(MapToDto).ToList();
        }

        private static GenreDto MapToDto(Genre genre)
        {
            return new GenreDto
            {
                Id = genre.Id,
                Name = genre.Name,
            };
        }
    }
}
