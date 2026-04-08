using Domain.Models;
using Service.DTO.Category;
using Service.Interfaces.Repositories;

namespace Service.Services;

public class CategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserRepository _userRepository;

    public CategoryService(ICategoryRepository categoryRepository, IUserRepository userRepository)
    {
        _categoryRepository = categoryRepository;   
        _userRepository = userRepository;
    }

    public async Task<CategoryDto> CreateCategory(CreateCategoryDto categoryDto, Guid userId)
    {
        await GetAdminUser(userId);
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
    
    private async Task GetAdminUser(Guid userId)
    {
        var user = await _userRepository.GetUserById(userId);

        if (user == null)
            throw new Exception("User not found");

        if (user.Role != UserRole.Admin)
            throw new Exception("Access denied");
    }
}