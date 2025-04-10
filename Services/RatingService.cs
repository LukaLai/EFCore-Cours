using MyWebAPI.Dtos;
using MyWebAPI.Models;
using MyWebAPI.Repositories;

namespace MyWebAPI.Services
{
    public class RatingService
    {
        private readonly IRepository<Rating> _ratingRepository;

        public RatingService(IRepository<Rating> ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task<IEnumerable<Rating>> GetAllRatingsAsync()
        {
            return await _ratingRepository.GetAllAsync();
        }

        public async Task<Rating?> GetRatingByIdAsync(int id)
        {
            return await _ratingRepository.GetByIdAsync(id);
        }

        public async Task AddRatingAsync(RatingDto ratingDto)
        {
            var rating = new Rating
            {
                SessionId = ratingDto.SessionId,
                ParticipantId = ratingDto.ParticipantId,
                Score = ratingDto.Score,
                Comment = ratingDto.Comment
            };
            await _ratingRepository.AddAsync(rating);
        }

        public async Task UpdateRatingAsync(int id, RatingDto ratingDto)
        {
            var rating = await _ratingRepository.GetByIdAsync(id);
            if (rating == null) return;

            rating.SessionId = ratingDto.SessionId;
            rating.ParticipantId = ratingDto.ParticipantId;
            rating.Score = ratingDto.Score;
            rating.Comment = ratingDto.Comment;

            await _ratingRepository.UpdateAsync(rating);
        }

        public async Task DeleteRatingAsync(int id)
        {
            var rating = await _ratingRepository.GetByIdAsync(id);
            if (rating != null)
            {
                await _ratingRepository.DeleteAsync(rating.Id);
            }
        }
    }
}