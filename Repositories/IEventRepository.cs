using MyWebAPI.Models;

namespace MyWebAPI.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<IEnumerable<Event>> GetEventsWithLocationAsync();
    }
}