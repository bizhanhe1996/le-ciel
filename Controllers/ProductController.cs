using LeCiel.Database.Repositories;
using LeCiel.DTOs.Requests;
using LeCiel.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LeCiel.Controllers;

[ApiController, Route("api/[controller]")]
public class ProductController(ProductsRepository productsRepository) : BaseController
{
    private readonly ProductsRepository _productsRepository = productsRepository;

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateRequestDto productCreateRequestDto)
    {
        var insertedProduct = await _productsRepository.CreateAsync(
            productCreateRequestDto.GetModel()
        );
        var response = new GenericResponse<ProductResponseSimpleDto>(
            insertedProduct != null,
            insertedProduct?.GetSimpleDto()
        );
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productsRepository.IndexAsync();
        var response = new GenericResponse<List<ProductResponseSimpleDto>>(
            true,
            [.. products.Select(p => p.GetSimpleDto())]
        );
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Find([FromRoute] uint id)
    {
        var product = await _productsRepository.FindAsync(id);
        var response = new GenericResponse<ProductResponseSimpleDto?>(
            product != null,
            product?.GetSimpleDto()
        );
        return Ok(response);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] uint id,
        [FromBody] ProductUpdateRequestDto productUpdateRequestDto
    )
    {
        var result = await _productsRepository.UpdateAsync(id, productUpdateRequestDto);
        var reponse = new GenericResponse<ProductResponseSimpleDto?>(
            result != null,
            result?.GetSimpleDto()
        );
        return Ok(reponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(uint id)
    {
        var result = await _productsRepository.DeleteAsync(id);
        var response = new GenericResponse<ProductResponseSimpleDto?>(
            result is not null,
            result?.GetSimpleDto()
        );
        return Ok(response);
    }
}
