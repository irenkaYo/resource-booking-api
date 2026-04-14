namespace Domain.Models;

public class Resource
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid LocationId { get; set; }
    public Guid CategoryId { get; set; }
    public int Capacity { get; set; }
    public bool IsActive { get; set; }
    public List<ResourceFeature> ResourceFeatures { get; set; }
    public uint xmin { get; set; }
    public Location Location { get; set; }
    public Category Category { get; set; }

    public Resource(string name, string description, Guid locationId, Guid categoryId, int capacity)
    {
        Id  = Guid.NewGuid();
        Name = name;
        Description = description;
        LocationId = locationId;
        CategoryId = categoryId;
        Capacity = capacity;
        IsActive = true;
        ResourceFeatures = new List<ResourceFeature>();
    }
}