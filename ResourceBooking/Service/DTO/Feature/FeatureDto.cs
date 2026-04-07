namespace Service.DTO.Feature;

public class FeatureDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public FeatureDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}