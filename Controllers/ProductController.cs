namespace LeCiel.Controllers;

using LeCiel.Database.Repositories;
using LeCiel.DTOs.Requests.Product;
using LeCiel.DTOs.Responses;
using LeCiel.DTOs.Responses.Product;
using Microsoft.AspNetCore.Mvc;

[ApiController, Route("api/[controller]")]
public class ProductController(ProductsRepository productsRepository) : BaseController
{
    private readonly ProductsRepository _productsRepository = productsRepository;

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

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest createProductRequest)
    {
        var product = await _productsRepository.CreateAsync(createProductRequest.GetModel());
        var response = new GenericResponse<ProductResponseDto>(true, product.GetDto());
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
        [FromBody] UpdateProductRequest updateProductRequest
    )
    {
        var result = await _productsRepository.UpdateAsync(id, updateProductRequest);
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
