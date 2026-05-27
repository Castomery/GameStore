using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameVault.Application.Interfaces.Services;
using GameVault.Application.Dtos.Game;

namespace GameVault.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameDto>>> GetAllGames()
        {
            var games = await _gameService.GetAllGamesAsync();
            return Ok(games);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGameById(int id)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            if (game == null)
            {
                return NotFound($"Game with ID {id} not found.");
            }
            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<GameDto>> CreateGame([FromBody] CreateGameDto createGameDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var created = await _gameService.CreateGameAsync(createGameDto);
                return CreatedAtAction(nameof(GetGameById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GameDto>> UpdateGame(int id, [FromBody] UpdateGameDto updateGameDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updated = await _gameService.UpdateGameAsync(id, updateGameDto);
                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGame(int id)
        {
            var result = await _gameService.DeleteGameAsync(id);
            if (!result)
            {
                return NotFound($"Game with ID {id} not found.");
            }
            return NoContent();
        }

        [HttpGet("genre/{genreId}")]
        public async Task<ActionResult<List<GameDto>>> GetGamesByGenre(int genreId)
        {
            var games = await _gameService.GetGamesByGenreAsync(genreId);
            return Ok(games);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<GameDto>>> SearchGames([FromQuery] string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest("Title query parameter is required.");
            }

            var games = await _gameService.SearchGamesByTitleAsync(title);
            return Ok(games);
        }
    }
}
