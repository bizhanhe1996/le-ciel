using LeCiel.Database.Models;
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
        var response = new GenericResponse<CategoryResponseSimpleDto>(
            insertedCategory is not null,
            insertedCategory?.GetSimpleDto()
        );
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categories = await _categoriesRepository.IndexAsync();
        var response = new GenericResponse<List<CategoryResponseSimpleDto>>(
            true,
            [.. categories.Select(c => c.GetSimpleDto())]
        );
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Find([FromRoute] int id)
    {
        var category = await _categoriesRepository.FindAsync((uint)id);
        var response = new GenericResponse<CategoryResponseSimpleDto?>(
            category is not null,
            category?.GetSimpleDto()
        );
        return Ok(response);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] CategoryUpdateRequestDto categoryUpdateRequestDto
    )
    {
        var result = await _categoriesRepository.UpdateAsync(id, categoryUpdateRequestDto);
        var response = new GenericResponse<CategoryResponseSimpleDto?>(
            result is not null,
            result?.GetSimpleDto()
        );
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _categoriesRepository.DeleteAsync(id);
        var response = new GenericResponse<CategoryResponseSimpleDto?>(
            result is not null,
            result?.GetSimpleDto()
        );
        return Ok(response);
    }

    [HttpGet("products/{id}")]
    public async Task<IActionResult> Products([FromRoute] int id)
    {
        var result = await _categoriesRepository.ProductsAsync(id);
        var response = new GenericResponse<ICollection<ProductResponseSimpleDto>?>(
            result is not null,
            [.. result.Select(p => p.GetSimpleDto())]
        );
        return Ok(response);
    }
}
