using Service.DTO.Category;

namespace Service.Interfaces.Services;

public interface ICategoryService
{
    public Task<CategoryDto> CreateCategory(CreateCategoryDto categoryDto, Guid userId);
    public Task<CategoryDto> GetCategoryById(Guid id);
}