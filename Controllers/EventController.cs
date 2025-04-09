using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MyWebAPI.Models;
using MyWebAPI.Services;
using MyWebAPI.Dtos;


namespace MyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        // GET: api/Event
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        // GET: api/Event/{id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var eventItem = await _eventService.GetEventByIdAsync(id);
            if (eventItem == null) return NotFound();
            return Ok(eventItem);
        }

        // POST: api/Event
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventCreateDto newEventDto)
        {
               var newEvent = new Event
                {
                    Title = newEventDto.Title,
                    Description = newEventDto.Description,
                    StartDate = newEventDto.StartDate,
                    EndDate = newEventDto.EndDate,
                    Status = newEventDto.Status,
                    Category = newEventDto.Category,
                    LocationId = newEventDto.LocationId
                };

            await _eventService.AddEventAsync(newEvent);
            return CreatedAtAction(nameof(GetEventById), new { id = newEvent.Id }, newEvent);
        }

        // PUT: api/Event/{id}
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, Event updatedEvent)
        {
            if (id != updatedEvent.Id) return BadRequest();

            var result = await _eventService.UpdateEventAsync(updatedEvent);
            if (!result) return NotFound();

            return NoContent();
        }

        // DELETE: api/Event/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var result = await _eventService.DeleteEventAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }

        // GET: api/Event/filter
        [Authorize]
        [HttpGet("filter")]
        public async Task<IActionResult> FilterEvents(
            [FromQuery] DateTime? startDate, 
            [FromQuery] DateTime? endDate, 
            [FromQuery] int? locationId, 
            [FromQuery] string? category, 
            [FromQuery] string? status)
        {
            var events = await _eventService.FilterEventsAsync(startDate, endDate, locationId, category, status);
            return Ok(events);
        }
    }
}