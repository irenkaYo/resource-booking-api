using Domain.Models;
using Service.DTO.Feature;
using Service.Interfaces.Repositories;
using Service.Interfaces.Services;

namespace Service.Services;

public class FeatureService : IFeatureService
{
    private readonly IFeatureRepository _featureRepository;
    private readonly IUserRepository _userRepository;

    public FeatureService(IFeatureRepository featureRepository, IUserRepository userRepository)
    {
        _featureRepository = featureRepository;
        _userRepository = userRepository;
    }

    public async Task<FeatureDto> CreateFeature(CreateFeatureDto featureDto, Guid userId)
    {
        await GetAdminUser(userId);
        Feature feature = new Feature(featureDto.Name);
        await _featureRepository.CreateFeature(feature);
        return ConvertFeatureToDto(feature);
    }

    public async Task<FeatureDto> GetFeatureById(Guid id)
    {
        Feature? feature = await _featureRepository.GetFeatureById(id);
        if (feature == null)
            throw new Exception("Feature not found");
        
        return ConvertFeatureToDto(feature);
    }

    private FeatureDto ConvertFeatureToDto(Feature feature)
    {
        FeatureDto dto = new FeatureDto(feature.Id, feature.Name);
        return dto;
    }
    
    private async Task GetAdminUser(Guid userId)
    {
        var user = await _userRepository.GetUserById(userId);

        if (user == null)
            throw new Exception("User not found");

        if (user.Role != UserRole.Admin)
            throw new Exception("Access denied");
    }
}