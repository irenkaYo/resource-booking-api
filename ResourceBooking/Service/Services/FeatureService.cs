using Domain.Models;
using Service.DTO.Feature;
using Service.Interfaces.Repositories;

namespace Service.Services;

public class FeatureService
{
    private readonly IFeatureRepository _featureRepository;

    public FeatureService(IFeatureRepository featureRepository)
    {
        _featureRepository = featureRepository;
    }

    public async Task<FeatureDto> CreateFeature(CreateFeatureDto featureDto)
    {
        Feature feature = new Feature(featureDto.Name);
        await _featureRepository.CreateFeature(feature);
        return ConvertFeatureToDto(feature);
    }

    private FeatureDto ConvertFeatureToDto(Feature feature)
    {
        FeatureDto dto = new FeatureDto(feature.Id, feature.Name);
        return dto;
    }
}