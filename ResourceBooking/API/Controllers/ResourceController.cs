using Infrastructure.DTO.Resource;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace API.Controllers;

[ApiController]
[Route("api/resources")]
public class ResourceController : ControllerBase
{
    private readonly ResourceService _resourceService;

    public ResourceController(ResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllResources()
    {
        var resources = await _resourceService.GetAllResources();
        return Ok(resources);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetResourceById(Guid id)
    {
        var resource = await _resourceService.GetResourceById(id);
        return Ok(resource);
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> CreateResource([FromBody] CreateResourceDto resourceDto, [FromRoute] Guid userId)
    {
        var resource = await _resourceService.CreateResource(resourceDto, userId);
        return Ok(resource);
    }

    [HttpPut("{resourceId}/{userId}")]
    public async Task<IActionResult> UpdateResource(Guid resourceId, [FromBody] UpdateResourceDto resourceDto, Guid userId)
    {
        var rowVersionBytes = Convert.FromBase64String(resourceDto.RowVersion);
        var resource = await _resourceService.UpdateResource(resourceId, userId, resourceDto, rowVersionBytes);
        return Ok(resource);
    }

    [HttpDelete("{id}/{userId}")]
    public async Task<IActionResult> DeleteResource(Guid id, Guid userId)
    {
        await _resourceService.DeleteResource(id, userId);
        return NoContent();
    }
    
    [HttpPost("filter")]
    public async Task<IActionResult> GetResources([FromBody]ResourceFilterDto filter)
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
}