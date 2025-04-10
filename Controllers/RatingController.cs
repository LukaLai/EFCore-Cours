using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Dtos;
using MyWebAPI.Services;

namespace MyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly RatingService _ratingService;

        public RatingController(RatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRatings()
        {
            var ratings = await _ratingService.GetAllRatingsAsync();
            return Ok(ratings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRatingById(int id)
        {
            var rating = await _ratingService.GetRatingByIdAsync(id);
            if (rating == null) return NotFound();
            return Ok(rating);
        }

        [HttpPost]
        public async Task<IActionResult> AddRating(RatingDto ratingDto)
        {
            await _ratingService.AddRatingAsync(ratingDto);
            return CreatedAtAction(nameof(GetRatingById), new { id = ratingDto.Id }, ratingDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRating(int id, RatingDto ratingDto)
        {
            await _ratingService.UpdateRatingAsync(id, ratingDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            await _ratingService.DeleteRatingAsync(id);
            return NoContent();
        }
    }
}