using Infrastructure.DTO.Resource;

namespace Service.Interfaces.Services;

public interface IResourceService
{
    public Task<List<ResourceDto>> GetAllResources();
    public Task<ResourceDto> GetResourceById(Guid id);
    public Task<ResourceDto> CreateResource(CreateResourceDto resourceDto, Guid userId);
    public Task<ResourceDto> UpdateResource(Guid resourceId, Guid userId, UpdateResourceDto dto);
    public Task DeleteResource(Guid resourceId, Guid userId);
    public Task<IEnumerable<ResourceDto>> GetFilterResources(ResourceFilterDto filter);
    public Task<bool> IsResourceFree(Guid resourceId, DateTimeOffset startDate, DateTimeOffset endDate);
    public Task AddFeature(Guid resourceId, Guid featureId);
    public Task DeactivateResource(Guid resourceId, Guid userId);
}