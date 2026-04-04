namespace Infrastructure.DTO.Resource;

public class ResourceFilterDto
{
    public Guid? LocationId { get; set; }
    public Guid? CategoryId { get; set; }
    public int? Capacity { get; set; }
    public Guid? FeatureId { get; set; }
}