using MyWebAPI.Dtos;
using MyWebAPI.Models;
using MyWebAPI.Repositories;

namespace MyWebAPI.Services
{
    public class SessionService
    {
        private readonly IRepository<Session> _sessionRepository;

        public SessionService(IRepository<Session> sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<IEnumerable<SessionDto>> GetAllSessionsAsync()
        {
            var sessions = await _sessionRepository.GetAllAsync();
            return sessions.Select(session => new SessionDto
            {
                Id = session.Id,
                Title = session.Title,
                Description = session.Description,
                StartDate = session.StartTime,
                EndDate = session.EndTime,
                EventId = session.EventId,
                RoomId = session.RoomId ?? 0
            });
        }

        public async Task<SessionDto?> GetSessionByIdAsync(int id)
        {
            var session = await _sessionRepository.GetByIdAsync(id);
            if (session == null) return null;

            return new SessionDto
            {
                Id = session.Id,
                Title = session.Title,
                Description = session.Description,
                StartDate = session.StartTime,
                EndDate = session.EndTime,
                EventId = session.EventId,
                RoomId = session.RoomId ?? 0
            };
        }

        public async Task AddSessionAsync(SessionDto sessionDto)
        {
            var session = new Session
            {
                Title = sessionDto.Title,
                Description = sessionDto.Description,
                StartTime = sessionDto.StartDate,
                EndTime = sessionDto.EndDate,
                EventId = sessionDto.EventId,
                RoomId = sessionDto.RoomId
            };

            await _sessionRepository.AddAsync(session);
        }

        public async Task UpdateSessionAsync(int id, SessionDto sessionDto)
        {
            var existingSession = await _sessionRepository.GetByIdAsync(id);
            if (existingSession == null) throw new Exception("Session not found");

            existingSession.Title = sessionDto.Title;
            existingSession.Description = sessionDto.Description;
            existingSession.StartTime = sessionDto.StartDate;
            existingSession.EndTime = sessionDto.EndDate;
            existingSession.EventId = sessionDto.EventId;
            existingSession.RoomId = sessionDto.RoomId;

            await _sessionRepository.UpdateAsync(existingSession);
        }

        public async Task DeleteSessionAsync(int id)
        {
            await _sessionRepository.DeleteAsync(id);
        }
    }
}