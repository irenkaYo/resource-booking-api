using Microsoft.AspNetCore.Mvc;
using Service.DTO.Category;
using Service.Services;

namespace API.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;   
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto, [FromHeader(Name = "X-User-Id")] Guid userId)
    {
        var category = await _categoryService.CreateCategory(categoryDto, userId);
        return CreatedAtAction(nameof(GetCategoryById), new { categoryId = category.Id }, category);
    }
    
    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetCategoryById(Guid categoryId)
    {
        var category = await _categoryService.GetCategoryById(categoryId);
        return Ok(category);
    }
}