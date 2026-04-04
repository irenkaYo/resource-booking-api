namespace Infrastructure.DTO.Resource;

public class UpdateResourceDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid LocationId { get; set; }
    public Guid CategoryId { get; set; }
    public int Capacity { get; set; }
}