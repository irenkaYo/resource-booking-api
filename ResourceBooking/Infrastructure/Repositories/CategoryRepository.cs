using Domain.Models;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ResourceBookingContext db;

    public CategoryRepository(ResourceBookingContext context)
    {
        db = context;
    }
    
    public async Task CreateCategory(Category category)
    {
        await db.Categories.AddAsync(category);
        await db.SaveChangesAsync();
    }

    public async Task<Category?> GetCategoryById(Guid categoryId)
    {
        Category? category = await db.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        return category;
    }
}