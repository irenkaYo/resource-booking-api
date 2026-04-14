using Service.DTO.Feature;

namespace Infrastructure.DTO.Resource;

public class ResourceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid LocationId { get; set; }
    public Guid CategoryId { get; set; }
    public int Capacity { get; set; }
    public bool IsActive { get; set; }
    public uint xmin { get; set; }
    public List<FeatureNameDto> Features { get; set; }

    public ResourceDto(
        Guid id, 
        string name, 
        string description, 
        Guid locationId, 
        Guid categoryId, 
        int capacity, 
        bool isActive, 
        uint xmin,
        List<FeatureNameDto> features)
    {
        Id = id;
        Name = name;
        Description = description;
        LocationId = locationId;
        CategoryId = categoryId;
        Capacity = capacity;
        IsActive = isActive;
        this.xmin = xmin;
        Features = features;
    }
}