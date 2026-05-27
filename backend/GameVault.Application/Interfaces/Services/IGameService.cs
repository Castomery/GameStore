using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameVault.Application.Dtos.Game;

namespace GameVault.Application.Interfaces.Services
{
    public interface IGameService
    {
        Task<List<GameDto>> GetAllGamesAsync();
        Task<GameDto?> GetGameByIdAsync(int id);
        Task<GameDto> CreateGameAsync(CreateGameDto createGameDto);
        Task<GameDto> UpdateGameAsync(int id, UpdateGameDto updateGameDto);
        Task<bool> DeleteGameAsync(int id);
        Task<List<GameDto>> GetGamesByGenreAsync(int genreId);
        Task<List<GameDto>> SearchGamesByTitleAsync(string title);
    }
}
