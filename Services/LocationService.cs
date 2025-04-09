using MyWebAPI.Dtos;
using MyWebAPI.Models;
using MyWebAPI.Repositories;

namespace MyWebAPI.Services
{
    public class LocationService
    {
        private readonly IRepository<Location> _locationRepository;

        public LocationService(IRepository<Location> locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<IEnumerable<LocationDto>> GetAllLocationsAsync()
        {
            var locations = await _locationRepository.GetAllAsync();
            return locations.Select(location => new LocationDto
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address,
                City = location.City,
                Country = location.Country,
                Capacity = location.Capacity
            });
        }

        public async Task<LocationDto?> GetLocationByIdAsync(int id)
        {
            var location = await _locationRepository.GetByIdAsync(id);
            if (location == null) return null;

            return new LocationDto
            {
                Id = location.Id,
                Name = location.Name,
                Address = location.Address,
                City = location.City,
                Country = location.Country,
                Capacity = location.Capacity
            };
        }

        public async Task AddLocationAsync(LocationDto locationDto)
        {
            var location = new Location
            {
                Name = locationDto.Name,
                Address = locationDto.Address,
                City = locationDto.City,
                Country = locationDto.Country,
                Capacity = locationDto.Capacity
            };

            await _locationRepository.AddAsync(location);
        }

        public async Task UpdateLocationAsync(int id, LocationDto locationDto)
        {
            var existingLocation = await _locationRepository.GetByIdAsync(id);
            if (existingLocation == null) throw new Exception("Location not found");

            existingLocation.Name = locationDto.Name;
            existingLocation.Address = locationDto.Address;
            existingLocation.City = locationDto.City;
            existingLocation.Country = locationDto.Country;
            existingLocation.Capacity = locationDto.Capacity;

            await _locationRepository.UpdateAsync(existingLocation);
        }

        public async Task DeleteLocationAsync(int id)
        {
            await _locationRepository.DeleteAsync(id);
        }
    }
}