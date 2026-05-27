using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameVault.Application.Dtos.Game;
using GameVault.Application.Interfaces.Repositories;
using GameVault.Application.Interfaces.Services;
using GameVault.Domain.Models;

namespace GameVault.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<GameDto>> GetAllGamesAsync()
        {
            var games = await _gameRepository.GetAllAsync();
            return games.Select(MapToDto).ToList();
        }

        public async Task<GameDto?> GetGameByIdAsync(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            return game == null ? null : MapToDto(game);
        }

        public async Task<GameDto> CreateGameAsync(CreateGameDto createGameDto)
        {
            var game = new Game
            {
                Title = createGameDto.Title,
                Description = createGameDto.Description,
                Price = createGameDto.Price,
                ReleaseDate = createGameDto.ReleaseDate,
                CoverImageUrl = createGameDto.CoverImageUrl,
                GenreId = createGameDto.GenreId,
                CreatedAt = DateTime.UtcNow
            };

            var createdGame = await _gameRepository.AddAsync(game);
            
            var fullyLoadedGame = await _gameRepository.GetByIdAsync(createdGame.Id);
            return MapToDto(fullyLoadedGame!);
        }

        public async Task<GameDto> UpdateGameAsync(int id, UpdateGameDto updateGameDto)
        {
            var game = new Game
            {
                Id = id,
                Title = updateGameDto.Title,
                Description = updateGameDto.Description,
                Price = updateGameDto.Price,
                ReleaseDate = updateGameDto.ReleaseDate,
                CoverImageUrl = updateGameDto.CoverImageUrl,
                GenreId = updateGameDto.GenreId
            };

            var updatedGame = await _gameRepository.UpdateAsync(id, game);

            var fullyLoadedGame = await _gameRepository.GetByIdAsync(updatedGame.Id);
            return MapToDto(fullyLoadedGame!);
        }

        public async Task<bool> DeleteGameAsync(int id)
        {
            return await _gameRepository.DeleteAsync(id);
        }

        public async Task<List<GameDto>> GetGamesByGenreAsync(int genreId)
        {
            var games = await _gameRepository.GetByGenreAsync(genreId);
            return games.Select(MapToDto).ToList();
        }

        public async Task<List<GameDto>> SearchGamesByTitleAsync(string title)
        {
            var games = await _gameRepository.SearchByTitleAsync(title);
            return games.Select(MapToDto).ToList();
        }

        private static GameDto MapToDto(Game game)
        {
            return new GameDto
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate,
                CoverImageUrl = game.CoverImageUrl,
                GenreName = game.Genre?.Name ?? string.Empty
            };
        }
    }
}
