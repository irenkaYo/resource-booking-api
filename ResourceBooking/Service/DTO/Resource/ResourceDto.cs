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
    public byte[] RowVersion { get; set; }

    public ResourceDto(
        Guid id, 
        string name, 
        string description, 
        Guid locationId, 
        Guid categoryId, 
        int capacity, 
        bool isActive, 
        byte[] rowVersion)
    {
        Id = id;
        Name = name;
        Description = description;
        LocationId = locationId;
        CategoryId = categoryId;
        Capacity = capacity;
        IsActive = isActive;
        RowVersion = rowVersion;
    }
}