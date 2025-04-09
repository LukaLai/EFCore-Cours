using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Dtos;
using MyWebAPI.Services;

namespace MyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly SessionService _sessionService;

        public SessionController(SessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSessions()
        {
            var sessions = await _sessionService.GetAllSessionsAsync();
            return Ok(sessions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSessionById(int id)
        {
            var session = await _sessionService.GetSessionByIdAsync(id);
            if (session == null) return NotFound();
            return Ok(session);
        }

        [HttpPost]
        public async Task<IActionResult> AddSession(SessionDto sessionDto)
        {
            await _sessionService.AddSessionAsync(sessionDto);
            return CreatedAtAction(nameof(GetSessionById), new { id = sessionDto.Id }, sessionDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSession(int id, SessionDto sessionDto)
        {
            await _sessionService.UpdateSessionAsync(id, sessionDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            await _sessionService.DeleteSessionAsync(id);
            return NoContent();
        }
    }
}