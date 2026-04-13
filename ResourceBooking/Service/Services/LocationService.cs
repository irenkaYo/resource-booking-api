using Domain.Models;
using Service.DTO.Location;
using Service.Interfaces.Repositories;

namespace Service.Services;

public class LocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly IUserRepository _userRepository;

    public LocationService(ILocationRepository locationRepository, IUserRepository userRepository)
    {
        _locationRepository = locationRepository;
        _userRepository = userRepository;
    }

    public async Task<LocationDto> CreateLocation(CreateLocationDto locationDto, Guid userId)
    {
        await GetAdminUser(userId);
        Location location = new Location(locationDto.Name);
        await _locationRepository.CreateLocation(location);
        return ConvertLocationToDto(location);
    }

    public async Task<LocationDto> GetLocationById(Guid id)
    {
        Location? location = await _locationRepository.GetLocationById(id);
        if (location == null)
            throw new Exception("Location not found");
        
        return ConvertLocationToDto(location);
    }

    private LocationDto ConvertLocationToDto(Location location)
    {
        LocationDto dto = new LocationDto(location.Id, location.Name);
        return dto;
    }
    
    private async Task GetAdminUser(Guid userId)
    {
        User? user = await _userRepository.GetUserById(userId);

        if (user == null)
            throw new Exception("User not found");

        if (user.Role != UserRole.Admin)
            throw new Exception("Access denied");
    }
}