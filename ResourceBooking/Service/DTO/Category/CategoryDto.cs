namespace Service.DTO.Category;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public CategoryDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}