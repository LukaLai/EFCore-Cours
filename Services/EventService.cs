using Microsoft.EntityFrameworkCore;
using MyWebAPI.Data;
using MyWebAPI.Models;

namespace MyWebAPI.Services
{
    public class EventService
    {
        private readonly ApplicationDbContext _context;

        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Récupérer tous les événements
        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Events.Include(e => e.Location).ToListAsync();
        }

        // Récupérer un événement par son ID
        public async Task<Event?> GetEventByIdAsync(int id)
        {
            return await _context.Events.Include(e => e.Location).FirstOrDefaultAsync(e => e.Id == id);
        }

        // Ajouter un nouvel événement
        public async Task AddEventAsync(Event newEvent)
        {
            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();
        }

        // Mettre à jour un événement existant
        public async Task<bool> UpdateEventAsync(Event updatedEvent)
        {
            var existingEvent = await _context.Events.FindAsync(updatedEvent.Id);
            if (existingEvent == null) return false;

            existingEvent.Title = updatedEvent.Title;
            existingEvent.Description = updatedEvent.Description;
            existingEvent.StartDate = updatedEvent.StartDate;
            existingEvent.EndDate = updatedEvent.EndDate;
            existingEvent.Status = updatedEvent.Status;
            existingEvent.Category = updatedEvent.Category;
            existingEvent.LocationId = updatedEvent.LocationId;

            await _context.SaveChangesAsync();
            return true;
        }

        // Supprimer un événement
        public async Task<bool> DeleteEventAsync(int id)
        {
            var eventToDelete = await _context.Events.FindAsync(id);
            if (eventToDelete == null) return false;

            _context.Events.Remove(eventToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
        //Pagination
        public async Task<IEnumerable<Event>> GetPaginatedEventsAsync(int pageNumber, int pageSize)
        {
            return await _context.Events
                .Include(e => e.Location) 
                .Skip((pageNumber - 1) * pageSize) 
                .Take(pageSize) 
                .ToListAsync();
        }   
        
        // Filtres
        public async Task<IEnumerable<Event>> FilterEventsAsync(DateTime? startDate, DateTime? endDate, int? locationId, string? category, string? status)
        {
            var query = _context.Events.AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(e => e.StartDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(e => e.EndDate <= endDate.Value);
            }

            if (locationId.HasValue)
            {
                query = query.Where(e => e.LocationId == locationId.Value);
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(e => e.Category == category);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(e => e.Status == status);
            }

            return await query.Include(e => e.Location).ToListAsync();
        }
    }


}