using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Dtos;
using MyWebAPI.Services;

namespace MyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly LocationService _locationService;

        public LocationController(LocationService locationService)
        {
            _locationService = locationService;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllLocations()
        {
            var locations = await _locationService.GetAllLocationsAsync();
            return Ok(locations);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationById(int id)
        {
            var location = await _locationService.GetLocationByIdAsync(id);
            if (location == null) return NotFound();
            return Ok(location);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddLocation(LocationDto locationDto)
        {
            await _locationService.AddLocationAsync(locationDto);
            return CreatedAtAction(nameof(GetLocationById), new { id = locationDto.Id }, locationDto);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(int id, LocationDto locationDto)
        {
            await _locationService.UpdateLocationAsync(id, locationDto);
            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            await _locationService.DeleteLocationAsync(id);
            return NoContent();
        }
    }
}