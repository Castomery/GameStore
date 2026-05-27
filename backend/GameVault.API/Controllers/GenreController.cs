using GameVault.Application.Dtos.Genre;
using GameVault.Application.Interfaces.Services;
using GameVault.Application.Services;
using GameVault.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameVault.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<GenreDto>>> GetAllGenres()
        {
            var genres = await _genreService.GetAllGenresAsync();
            return Ok(genres);
        }

        [HttpPost]
        public async Task<ActionResult<GenreDto>> CreateGenre([FromBody] CreateGenreDto createGenreDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var created = await _genreService.CreateGenreAsync(createGenreDto);
                return Ok(created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            var result = await _genreService.DeleteGenreAsync(id);
            if (!result)
                return NotFound($"Genre with ID {id} not found.");
            return NoContent();
        }
    }
}
