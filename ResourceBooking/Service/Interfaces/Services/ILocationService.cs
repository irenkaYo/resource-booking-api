using Service.DTO.Location;

namespace Service.Interfaces.Services;

public interface ILocationService
{
    public Task<LocationDto> CreateLocation(CreateLocationDto locationDto, Guid userId);
    public Task<LocationDto> GetLocationById(Guid id);
}