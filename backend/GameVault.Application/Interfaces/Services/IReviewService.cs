using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameVault.Application.Dtos.Review;

namespace GameVault.Application.Interfaces.Services
{
    public interface IReviewService
    {
        Task<List<ReviewDto>> GetReviewsByGameIdAsync(int gameId);
        Task<ReviewDto> AddReviewAsync(int userId, int gameId, CreateReviewDto createReviewDto);
        Task<ReviewDto> UpdateReviewAsync(int userId, int reviewId, UpdateReviewDto updateReviewDto);
        Task<bool> DeleteReviewAsync(int userId, int reviewId);
    }
}
