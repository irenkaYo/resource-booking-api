using Microsoft.AspNetCore.Mvc;
using Service.DTO.Location;
using Service.Services;

namespace API.Controllers;

[ApiController]
[Route("api/locations")]
public class LocationController : ControllerBase
{
    private readonly LocationService _locationService;

    public LocationController(LocationService locationService)
    {
        _locationService = locationService;   
    }

    [HttpPost]
    public async Task<IActionResult> CreateLocation([FromBody] CreateLocationDto locationDto, [FromHeader(Name = "X-User-Id")] Guid userId)
    {
        var location = await _locationService.CreateLocation(locationDto, userId);
        return CreatedAtAction(nameof(GetLocationById), new { id = location.Id }, location);
    }
    
    [HttpGet("{locationId}")]
    public async Task<IActionResult> GetLocationById(Guid locationId)
    {
        var location = await _locationService.GetLocationById(locationId);
        return Ok(location);
    }
}