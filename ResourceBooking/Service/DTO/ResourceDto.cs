namespace Infrastructure.DTO;

public class ResourceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string LocationName { get; set; }
    public string CategoryName { get; set; }
    public int Capacity { get; set; }
    public bool IsActive { get; set; }
}