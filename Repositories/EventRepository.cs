using Microsoft.EntityFrameworkCore;
using MyWebAPI.Data;
using MyWebAPI.Models;

namespace MyWebAPI.Repositories
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetEventsWithLocationAsync()
        {
            return await _context.Events.Include(e => e.Location).ToListAsync();
        }
    }
}