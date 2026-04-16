using Domain.Models;
using Infrastructure.DTO.Resource;
using Microsoft.EntityFrameworkCore;
using Service.DTO.Feature;
using Service.Interfaces.Repositories;
using Service.Interfaces.Services;

namespace Service.Services;

public class ResourceService : IResourceService
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IFeatureRepository _featureRepository;
    private  readonly IResourceFeatureRepository _resourceFeatureRepository;
    
    public ResourceService(
        IResourceRepository resourceRepository, 
        IUserRepository userRepository, 
        IBookingRepository bookingRepository,
        IFeatureRepository featureRepository,
        IResourceFeatureRepository resourceFeatureRepository)
    {
        _resourceRepository = resourceRepository;
        _userRepository = userRepository;
        _bookingRepository = bookingRepository;
        _featureRepository = featureRepository;
        _resourceFeatureRepository = resourceFeatureRepository;
    }

    public async Task<List<ResourceDto>> GetAllResources()
    {
        List<Resource> resources = await _resourceRepository.GetAllResources();
        List<ResourceDto> resourceDtos = new List<ResourceDto>();
        foreach (Resource resource in resources)
        {
            ResourceDto resourceDto = ConvertResourceToResourceDto(resource);
            resourceDtos.Add(resourceDto);
        }
        return resourceDtos;
    }

    public async Task<ResourceDto> GetResourceById(Guid id)
    {
        Resource resource = await GetResource(id);
        return ConvertResourceToResourceDto(resource);
    }

    public async Task<ResourceDto> CreateResource(CreateResourceDto resourceDto, Guid userId)
    {
        await GetAdminUser(userId);
        
        Resource resource = new Resource(
            resourceDto.Name, 
            resourceDto.Description, 
            resourceDto.LocationId, 
            resourceDto.CategoryId, 
            resourceDto.Capacity);
        await _resourceRepository.CreateResource(resource);
        return ConvertResourceToResourceDto(resource);
    }
    
    public async Task<ResourceDto> UpdateResource(Guid resourceId, Guid userId, UpdateResourceDto dto)
    {
        await GetAdminUser(userId);
        
        Resource resource = await GetResource(resourceId);
        _resourceRepository.SetXmin(resource, dto.xmin);

        if (dto.Name != null)
            resource.Name = dto.Name;

        if (dto.LocationId.HasValue)
            resource.LocationId = dto.LocationId.Value;

        if (dto.CategoryId.HasValue)
            resource.CategoryId = dto.CategoryId.Value;

        if (dto.Capacity.HasValue)
            resource.Capacity = dto.Capacity.Value;

        try
        {
            await _resourceRepository.UpdateResource(resource);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new Exception("The resource has been modified by another user");
        }
        return ConvertResourceToResourceDto(resource);
    }

    public async Task DeleteResource(Guid resourceId, Guid userId)
    {
        await GetAdminUser(userId);
        Resource resource = await GetResource(resourceId);
        if (!resource.IsActive)
            await _resourceRepository.DeleteResource(resource);
        else
            throw new Exception("Resource is already active");
    }
    
    public async Task<IEnumerable<ResourceDto>> GetFilterResources(ResourceFilterDto filter)
    {
        var resources = await _resourceRepository.GetFilteredResources(
            filter.LocationId,
            filter.CategoryId,
            filter.Capacity,
            filter.FeatureId);

        var enumerable = resources.ToList();
        
        return enumerable.Select(r => ConvertResourceToResourceDto(r));
    }

    public async Task<bool> IsResourceFree(Guid resourceId, DateTimeOffset startDate, DateTimeOffset endDate)
    {
        var hasConflict = await _bookingRepository.HasConflict(resourceId, startDate, endDate);
        return !hasConflict;
    }

    public async Task AddFeature(Guid resourceId, Guid featureId)
    {
        Feature? feature = await _featureRepository.GetFeatureById(featureId);

        if (feature == null)
            throw new Exception("Feature not found");
        
        var exists = await _resourceFeatureRepository.Exists(resourceId, featureId);
        if (exists)
            throw new Exception("Feature already added to resource");

        ResourceFeature resourceFeature = new ResourceFeature(resourceId, featureId);

        await _resourceFeatureRepository.Add(resourceFeature);
    }

    public async Task DeactivateResource(Guid resourceId, Guid userId)
    {
        await GetAdminUser(userId);
        Resource resource = await GetResource(resourceId);
        resource.IsActive = false;
        await _resourceRepository.UpdateResource(resource);
    }

    private ResourceDto ConvertResourceToResourceDto(Resource resource)
    {
        var featureDtos = resource.ResourceFeatures
            .Select(rf => new FeatureNameDto(rf.Feature.Name))
            .ToList();
        
        ResourceDto resourceDto = new ResourceDto(
            resource.Id, 
            resource.Name, 
            resource.Description, 
            resource.LocationId, 
            resource.CategoryId, 
            resource.Capacity, 
            resource.IsActive,
            resource.xmin,
            featureDtos);
        return resourceDto;
    }
    
    private async Task GetAdminUser(Guid userId)
    {
        var user = await _userRepository.GetUserById(userId);

        if (user == null)
            throw new Exception("User not found");

        if (user.Role != UserRole.Admin)
            throw new Exception("Access denied");
    }
    
    private async Task<Resource> GetResource(Guid resourceId)
    {
        Resource? resource = await _resourceRepository.GetResourceById(resourceId);

        if (resource == null)
            throw new Exception("Resource not found");

        return resource;
    }
}