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

        [HttpGet]
        public async Task<IActionResult> GetAllLocations()
        {
            var locations = await _locationService.GetAllLocationsAsync();
            return Ok(locations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationById(int id)
        {
            var location = await _locationService.GetLocationByIdAsync(id);
            if (location == null) return NotFound();
            return Ok(location);
        }

        [HttpPost]
        public async Task<IActionResult> AddLocation(LocationDto locationDto)
        {
            await _locationService.AddLocationAsync(locationDto);
            return CreatedAtAction(nameof(GetLocationById), new { id = locationDto.Id }, locationDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(int id, LocationDto locationDto)
        {
            await _locationService.UpdateLocationAsync(id, locationDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            await _locationService.DeleteLocationAsync(id);
            return NoContent();
        }
    }
}