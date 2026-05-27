using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameVault.Application.Interfaces.Services;
using GameVault.Application.Dtos.Review;

namespace GameVault.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("game/{gameId}")]
        public async Task<ActionResult<List<ReviewDto>>> GetReviewsByGame(int gameId)
        {
            var reviews = await _reviewService.GetReviewsByGameIdAsync(gameId);
            return Ok(reviews);
        }

        [HttpPost("game/{gameId}")]
        public async Task<ActionResult<ReviewDto>> AddReview(
            int gameId, 
            [FromBody] CreateReviewDto createReviewDto,
            [FromHeader(Name = "X-User-Id")] int userId)
        {
            if (userId <= 0)
            {
                return Unauthorized("A valid X-User-Id header is required to submit a review.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var created = await _reviewService.AddReviewAsync(userId, gameId, createReviewDto);
                return CreatedAtAction(nameof(GetReviewsByGame), new { gameId = gameId }, created);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReviewDto>> UpdateReview(
            int id, 
            [FromBody] UpdateReviewDto updateReviewDto,
            [FromHeader(Name = "X-User-Id")] int userId)
        {
            if (userId <= 0)
            {
                return Unauthorized("A valid X-User-Id header is required to update a review.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updated = await _reviewService.UpdateReviewAsync(userId, id, updateReviewDto);
                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReview(
            int id,
            [FromHeader(Name = "X-User-Id")] int userId)
        {
            if (userId <= 0)
            {
                return Unauthorized("A valid X-User-Id header is required to delete a review.");
            }

            try
            {
                var result = await _reviewService.DeleteReviewAsync(userId, id);
                if (!result)
                {
                    return NotFound($"Review with ID {id} not found.");
                }
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
