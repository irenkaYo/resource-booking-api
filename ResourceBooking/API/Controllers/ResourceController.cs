using Infrastructure.DTO.Resource;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Services;

namespace API.Controllers;

[ApiController]
[Route("api/resources")]
public class ResourceController : ControllerBase
{
    private readonly IResourceService _resourceService;

    public ResourceController(IResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllResources()
    {
        var resources = await _resourceService.GetAllResources();
        return Ok(resources);
    }

    [HttpGet("{resourceId}")]
    public async Task<IActionResult> GetResourceById(Guid resourceId)
    {
        var resource = await _resourceService.GetResourceById(resourceId);
        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> CreateResource([FromBody] CreateResourceDto resourceDto, [FromHeader(Name = "X-User-Id")] Guid userId)
    {
        var resource = await _resourceService.CreateResource(resourceDto, userId);
        return Ok(resource);
    }

    [HttpPatch("{resourceId}")]
    public async Task<IActionResult> UpdateResource(Guid resourceId, [FromBody] UpdateResourceDto resourceDto, [FromHeader(Name = "X-User-Id")] Guid userId)
    {
        var resource = await _resourceService.UpdateResource(resourceId, userId, resourceDto);
        return Ok(resource);
    }

    [HttpDelete("{resourceId}")]
    public async Task<IActionResult> DeleteResource(Guid resourceId, [FromHeader(Name = "X-User-Id")] Guid userId)
    {
        await _resourceService.DeleteResource(resourceId, userId);
        return NoContent();
    }
    
    [HttpPost("filter")]
    public async Task<IActionResult> GetFilterResources([FromBody]ResourceFilterDto filter)
    {
        var resources = await _resourceService.GetFilterResources(filter);
        return Ok(resources);
    }

    [HttpGet("{resourceId}/availability")]
    public async Task<IActionResult> ResourceIsFree(Guid resourceId, [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        var result = await _resourceService.IsResourceFree(resourceId, startDate, endDate);
        return Ok(result);
    }

    [HttpPost("{resourceId}/feature")]
    public async Task<IActionResult> AddFeature(Guid resourceId, [FromBody] Guid featureId)
    {
        await _resourceService.AddFeature(resourceId, featureId);
        return Ok();
    }

    [HttpGet("{resourceId}/active")]
    public async Task<IActionResult> DeactivateResource(Guid resourceId, [FromHeader(Name = "X-User-Id")] Guid userId)
    {
        await _resourceService.DeactivateResource(resourceId, userId);
        return Ok("Resource deactivated");
    }
}