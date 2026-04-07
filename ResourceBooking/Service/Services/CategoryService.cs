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

    private CategoryDto ConvertCategoryToDto(Category category)
    {
        CategoryDto dto = new CategoryDto(category.Id, category.Name);
        return dto;
    }
}