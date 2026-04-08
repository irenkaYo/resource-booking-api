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
    public async Task<IActionResult> CreateLocation([FromBody] CreateLocationDto locationDto)
    {
        var location = await _locationService.CreateLocation(locationDto);
        return CreatedAtAction(nameof(GetLocationById), new { id = location.Id }, location);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLocationById(Guid id)
    {
        var location = await _locationService.GetLocationById(id);
        return Ok(location);
    }
}