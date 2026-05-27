using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameVault.Application.Dtos.Review;
using GameVault.Application.Interfaces.Repositories;
using GameVault.Application.Interfaces.Services;
using GameVault.Domain.Models;

namespace GameVault.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IGameRepository _gameRepository;

        public ReviewService(IReviewRepository reviewRepository, IGameRepository gameRepository)
        {
            _reviewRepository = reviewRepository;
            _gameRepository = gameRepository;
        }

        public async Task<List<ReviewDto>> GetReviewsByGameIdAsync(int gameId)
        {
            var reviews = await _reviewRepository.GetByGameIdAsync(gameId);
            return reviews.Select(MapToDto).ToList();
        }

        public async Task<ReviewDto> AddReviewAsync(int userId, int gameId, CreateReviewDto createReviewDto)
        {
            var game = await _gameRepository.GetByIdAsync(gameId);
            if (game == null)
            {
                throw new KeyNotFoundException($"Game with ID {gameId} not found");
            }

            var alreadyReviewed = await _reviewRepository.ReviewExistsAsync(userId, gameId);
            if (alreadyReviewed)
            {
                throw new InvalidOperationException("You have already reviewed this game.");
            }

            var review = new Review
            {
                UserId = userId,
                GameId = gameId,
                Text = createReviewDto.Text,
                Rating = createReviewDto.Rating,
                CreatedAt = DateTime.UtcNow
            };

            var createdReview = await _reviewRepository.AddAsync(review);

            var fullyLoadedReview = await _reviewRepository.GetByIdAsync(createdReview.Id);
            return MapToDto(fullyLoadedReview!);
        }

        public async Task<ReviewDto> UpdateReviewAsync(int userId, int reviewId, UpdateReviewDto updateReviewDto)
        {
            var existing = await _reviewRepository.GetByIdAsync(reviewId);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Review with ID {reviewId} not found");
            }

            if (existing.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this review.");
            }

            existing.Text = updateReviewDto.Text;
            existing.Rating = updateReviewDto.Rating;

            var updated = await _reviewRepository.UpdateAsync(reviewId, existing);
            return MapToDto(updated);
        }

        public async Task<bool> DeleteReviewAsync(int userId, int reviewId)
        {
            var existing = await _reviewRepository.GetByIdAsync(reviewId);
            if (existing == null)
            {
                return false;
            }

            if (existing.UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this review.");
            }

            return await _reviewRepository.DeleteAsync(reviewId);
        }

        private static ReviewDto MapToDto(Review review)
        {
            return new ReviewDto
            {
                Id = review.Id,
                Text = review.Text,
                Rating = review.Rating,
                CreatedAt = review.CreatedAt,
                UserName = review.User?.UserName ?? string.Empty
            };
        }
    }
}
