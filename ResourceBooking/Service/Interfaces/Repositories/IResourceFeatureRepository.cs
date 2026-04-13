using Domain.Models;

namespace Service.Interfaces.Repositories;

public interface IResourceFeatureRepository
{
    Task Add(ResourceFeature resourceFeature);
    Task<bool> Exists(Guid resourceId, Guid featureId);
}