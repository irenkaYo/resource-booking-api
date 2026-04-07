using Domain.Models;

namespace Service.Interfaces.Repositories;

public interface ILocationRepository
{
    public Task CreateLocation(Location location);
    public Task<Location?> GetLocationById(Guid locationId);
}