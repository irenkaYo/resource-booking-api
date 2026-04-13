namespace Domain.Models;

public class ResourceFeature
{
    public Guid ResourceId { get; set; }
    public Resource Resource { get; set; }

    public Guid FeatureId { get; set; }
    public Feature Feature { get; set; }

    public ResourceFeature(Guid resourceId, Guid featureId)
    {
        ResourceId = resourceId;
        FeatureId = featureId;
    }
}