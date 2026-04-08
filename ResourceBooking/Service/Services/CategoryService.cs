using Domain.Models;
using Service.DTO.Category;
using Service.Interfaces.Repositories;

namespace Service.Services;

public class CategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;   
    }

    public async Task<CategoryDto> CreateCategory(CreateCategoryDto categoryDto)
    {
        Category category = new Category(categoryDto.Name);
        await _categoryRepository.CreateCategory(category);
        return ConvertCategoryToDto(category);
    }

    public async Task<CategoryDto> GetCategoryById(Guid id)
    {
        Category? category = await _categoryRepository.GetCategoryById(id);
        if (category == null)
            throw new Exception("Category not found");
        
        return ConvertCategoryToDto(category);
    }

    private CategoryDto ConvertCategoryToDto(Category category)
    {
        CategoryDto dto = new CategoryDto(category.Id, category.Name);
        return dto;
    }
}