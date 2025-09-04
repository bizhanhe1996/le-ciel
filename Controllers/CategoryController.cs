using LeCiel.Database.Repositories;
using LeCiel.DTOs.Requests;
using LeCiel.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LeCiel.Controllers;

[ApiController, Route("/api/[controller]")]
public class CategoryController(CategoriesRepository categoriesRepository) : BaseController
{
    private readonly CategoriesRepository _categoriesRepository = categoriesRepository;

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CategoryCreateRequestDto categoryCreateRequestDto
    )
    {
        var insertedCategory = await _categoriesRepository.CreateAsync(
            categoryCreateRequestDto.GetModel()
        );
        var response = new GenericResponse<CategoryResponseDto>(true, insertedCategory.GetDto());
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categories = await _categoriesRepository.IndexAsync();
        var response = new GenericResponse<List<CategoryResponseDto>>(
            true,
            [.. categories.Select(c => c.GetDto())]
        );
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Find([FromRoute] int id)
    {
        var category = await _categoriesRepository.FindAsync((uint)id);
        var response = new GenericResponse<CategoryResponseDto>(
            category != null,
            category?.GetDto()
        );
        return Ok(response);
    }
}
