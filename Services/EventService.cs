using MyWebAPI.Dtos;
using MyWebAPI.Models;
using MyWebAPI.Repositories;

namespace MyWebAPI.Services
{
    public class EventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _eventRepository.GetEventsWithLocationAsync();
        }

        public async Task<Event?> GetEventByIdAsync(int id)
        {
            return await _eventRepository.GetByIdAsync(id);
        }

        public async Task AddEventAsync(EventCreateDto eventDto)
        {
            var newEvent = new Event
            {
                Title = eventDto.Title,
                Description = eventDto.Description,
                StartDate = eventDto.StartDate,
                EndDate = eventDto.EndDate,
                Status = eventDto.Status,
                Category = eventDto.Category,
                LocationId = eventDto.LocationId
            };

            await _eventRepository.AddAsync(newEvent);
        }

        public async Task UpdateEventAsync(int id, EventCreateDto eventDto)
        {
            var existingEvent = await _eventRepository.GetByIdAsync(id);
            if (existingEvent == null) throw new Exception("Event not found");

            existingEvent.Title = eventDto.Title;
            existingEvent.Description = eventDto.Description;
            existingEvent.StartDate = eventDto.StartDate;
            existingEvent.EndDate = eventDto.EndDate;
            existingEvent.Status = eventDto.Status;
            existingEvent.Category = eventDto.Category;
            existingEvent.LocationId = eventDto.LocationId;

            await _eventRepository.UpdateAsync(existingEvent);
        }

        public async Task DeleteEventAsync(int id)
        {
            await _eventRepository.DeleteAsync(id);
        }
    }
}