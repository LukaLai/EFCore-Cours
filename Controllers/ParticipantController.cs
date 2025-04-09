using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Dtos;
using MyWebAPI.Services;

namespace MyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantController : ControllerBase
    {
        private readonly ParticipantService _participantService;

        public ParticipantController(ParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParticipants()
        {
            var participants = await _participantService.GetAllParticipantsAsync();
            return Ok(participants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetParticipantById(int id)
        {
            var participant = await _participantService.GetParticipantByIdAsync(id);
            if (participant == null) return NotFound();
            return Ok(participant);
        }

        [HttpPost]
        public async Task<IActionResult> AddParticipant(ParticipantDto participantDto)
        {
            await _participantService.AddParticipantAsync(participantDto);
            return CreatedAtAction(nameof(GetParticipantById), new { id = participantDto.Id }, participantDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParticipant(int id, ParticipantDto participantDto)
        {
            await _participantService.UpdateParticipantAsync(id, participantDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipant(int id)
        {
            await _participantService.DeleteParticipantAsync(id);
            return NoContent();
        }
    }
}