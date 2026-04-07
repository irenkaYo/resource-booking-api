using Domain.Models;

namespace Service.Interfaces.Repositories;

public interface IFeatureRepository
{
    public Task CreateFeature(Feature feature);
    public Task<Feature?> GetFeatureById(Guid featureId);
}