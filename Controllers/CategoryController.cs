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
        var response = new GenericResponse<CategoryResponseDto>(
            insertedCategory != null,
            insertedCategory?.GetDto()
        );
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
        var response = new GenericResponse<CategoryResponseDto?>(
            category != null,
            category?.GetDto()
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
        var response = new GenericResponse<CategoryResponseDto?>(result != null, result?.GetDto());
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _categoriesRepository.DeleteAsync(id);
        var response = new GenericResponse<CategoryResponseDto?>(result != null, result?.GetDto());
        return Ok(response);
    }

    [HttpGet("products/{id}")]
    public async Task<IActionResult> Products(int id)
    {
        var result = await _categoriesRepository.ProductsAsync(id);
        var response = new GenericResponse<ICollection<Product>?>(true, result);
        return Ok(response);
    }
}
