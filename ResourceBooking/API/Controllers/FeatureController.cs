using Microsoft.AspNetCore.Mvc;
using Service.DTO.Feature;
using Service.Services;

namespace API.Controllers;

[ApiController]
[Route("api/features")]
public class FeatureController : ControllerBase
{
    private readonly FeatureService _featureService;

    public FeatureController(FeatureService featureService)
    {
        _featureService = featureService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateFeature([FromBody] CreateFeatureDto featureDto, [FromHeader(Name = "X-User-Id")] Guid userId)
    {
        var feature = await _featureService.CreateFeature(featureDto, userId);
        return CreatedAtAction(nameof(GetFeatureById), new { id = feature.Id }, feature);
    }
    
    [HttpGet("{featureId}")]
    public async Task<IActionResult> GetFeatureById(Guid featureId)
    {
        var feature = await _featureService.GetFeatureById(featureId);
        return Ok(feature);
    }
}