using Domain.Models;

namespace Service.Interfaces.Repositories;

public interface IResourceRepository
{
    public Task<List<Resource>> GetAllResources();
    public Task<Resource?> GetResourceById(Guid resourceId);
    public Task CreateResource(Resource resource);
    public Task UpdateResource(Resource resource, byte[] rowVersion);
    public Task UpdateResource(Resource resource);
    public Task DeleteResource(Resource resource);
    public Task<IEnumerable<Resource>> GetFilteredResources(Guid? locationId, Guid? categoryId, int? capacity, Guid? featureId);
}