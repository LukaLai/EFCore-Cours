using MyWebAPI.Dtos;
using MyWebAPI.Models;
using MyWebAPI.Repositories;

namespace MyWebAPI.Services
{
    public class RoomService
    {
        private readonly IRepository<Room> _roomRepository;

        public RoomService(IRepository<Room> roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<RoomDto>> GetAllRoomsAsync()
        {
            var rooms = await _roomRepository.GetAllAsync();
            return rooms.Select(room => new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Capacity = room.Capacity,
                LocationId = room.LocationId
            });
        }

        public async Task<RoomDto?> GetRoomByIdAsync(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null) return null;

            return new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Capacity = room.Capacity,
                LocationId = room.LocationId
            };
        }

        public async Task AddRoomAsync(RoomDto roomDto)
        {
            var room = new Room
            {
                Name = roomDto.Name,
                Capacity = roomDto.Capacity,
                LocationId = roomDto.LocationId
            };

            await _roomRepository.AddAsync(room);
        }

        public async Task UpdateRoomAsync(int id, RoomDto roomDto)
        {
            var existingRoom = await _roomRepository.GetByIdAsync(id);
            if (existingRoom == null) throw new Exception("Room not found");

            existingRoom.Name = roomDto.Name;
            existingRoom.Capacity = roomDto.Capacity;
            existingRoom.LocationId = roomDto.LocationId;

            await _roomRepository.UpdateAsync(existingRoom);
        }

        public async Task DeleteRoomAsync(int id)
        {
            await _roomRepository.DeleteAsync(id);
        }
    }
}