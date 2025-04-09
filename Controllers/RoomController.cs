using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Dtos;
using MyWebAPI.Services;

namespace MyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly RoomService _roomService;

        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null) return NotFound();
            return Ok(room);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom(RoomDto roomDto)
        {
            await _roomService.AddRoomAsync(roomDto);
            return CreatedAtAction(nameof(GetRoomById), new { id = roomDto.Id }, roomDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, RoomDto roomDto)
        {
            await _roomService.UpdateRoomAsync(id, roomDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            await _roomService.DeleteRoomAsync(id);
            return NoContent();
        }
    }
}