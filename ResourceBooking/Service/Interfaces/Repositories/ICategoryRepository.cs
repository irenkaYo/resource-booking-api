using Domain.Models;

namespace Service.Interfaces.Repositories;

public interface ICategoryRepository
{
    public Task CreateCategory(Category category);
    public Task<Category?> GetCategoryById(Guid categoryId);
}