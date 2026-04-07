using Domain.Models;
using Service.DTO.Location;
using Service.Interfaces.Repositories;

namespace Service.Services;

public class LocationService
{
    private readonly ILocationRepository _locationRepository;

    public LocationService(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<LocationDto> CreateLocation(CreateLocationDto locationDto)
    {
        Location location = new Location(locationDto.Name);
        await _locationRepository.CreateLocation(location);
        return ConvertLocationToDto(location);
    }

    private LocationDto ConvertLocationToDto(Location location)
    {
        LocationDto dto = new LocationDto(location.Id, location.Name);
        return dto;
    }
}