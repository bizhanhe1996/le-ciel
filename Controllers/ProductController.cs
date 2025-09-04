namespace LeCiel.Controllers;

using LeCiel.Database.Repositories;
using LeCiel.DTOs.Requests;
using LeCiel.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

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
        var response = new GenericResponse<ProductResponseDto>(true, insertedProduct.GetDto());
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productsRepository.IndexAsync();
        var response = new GenericResponse<List<ProductResponseDto>>(
            true,
            [.. products.Select(p => p.GetDto())]
        );
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Find([FromRoute] uint id)
    {
        var product = await _productsRepository.FindAsync(id);
        var response = new GenericResponse<ProductResponseDto>(true, product?.GetDto());
        return Ok(response);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] uint id,
        [FromBody] ProductUpdateRequestDto productUpdateRequestDto
    )
    {
        var result = await _productsRepository.UpdateAsync(id, productUpdateRequestDto);
        var reponse = new GenericResponse<ProductResponseDto>(result != null, result?.GetDto());
        return Ok(reponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(uint id)
    {
        var result = await _productsRepository.DeleteAsync(id);
        var response = new GenericResponse<ProductResponseDto>(result != null, result?.GetDto());
        return Ok(response);
    }
}
