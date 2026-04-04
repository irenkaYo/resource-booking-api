using System.ComponentModel.Design;
using Domain.Models;
using Infrastructure.DTO.Resource;
using Infrastructure.InterfacesRepositories;

namespace Service.Services;

public class ResourceService
{
    private readonly IResourceRepository _resourceRepository;
    private readonly IUserRepository _userRepository;
    
    public ResourceService(IResourceRepository resourceRepository, IUserRepository userRepository)
    {
        _resourceRepository = resourceRepository;
        _userRepository = userRepository;
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

    public async Task<ResourceDto> UpdateResource(Guid resourceId, Guid userId, UpdateResourceDto resourceDto)
    {
        await GetAdminUser(userId);
        
        Resource resource = await GetResource(resourceId);

        resource.Name = resourceDto.Name;
        resource.Description = resourceDto.Description;
        resource.LocationId = resourceDto.LocationId;
        resource.CategoryId = resourceDto.CategoryId;
        resource.Capacity = resourceDto.Capacity;
        
        await _resourceRepository.UpdateResource(resource);
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

    private ResourceDto ConvertResourceToResourceDto(Resource resource)
    {
        ResourceDto resourceDto = new ResourceDto(
            resource.Id, 
            resource.Name, 
            resource.Description, 
            resource.LocationId, 
            resource.CategoryId, 
            resource.Capacity, 
            resource.IsActive);
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
        var resource = await _resourceRepository.GetResourceById(resourceId);

        if (resource == null)
            throw new Exception("Resource not found");

        return resource;
    }
}