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
    //RowVersion
}