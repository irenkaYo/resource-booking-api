using Domain.Models;

namespace Infrastructure.InterfacesRepositories;

public interface IResourceRepository
{
    public Task<List<Resource>> GetAllResources();
    public Task<Resource>? GetResourceById(Guid resourceId);
    public Task CreateResource(Resource resource);
    public Task UpdateResource(Resource resource);
    public Task DeleteResource(Resource resource);
}