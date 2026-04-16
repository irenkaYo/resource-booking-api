using Service.DTO.Feature;

namespace Service.Interfaces.Services;

public interface IFeatureService
{
    public Task<FeatureDto> CreateFeature(CreateFeatureDto featureDto, Guid userId);
    public Task<FeatureDto> GetFeatureById(Guid id);
}