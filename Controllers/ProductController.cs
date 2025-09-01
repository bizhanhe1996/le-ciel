namespace LeCiel.Controllers;

using LeCiel.Database.Repositories;
using LeCiel.DTOs.Requests.Product;
using Microsoft.AspNetCore.Mvc;

[ApiController, Route("api/[controller]")]
public class ProductController(ProductsRepository productsRepository) : BaseController
{
    private readonly ProductsRepository _productsRepository = productsRepository;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productsRepository.GetAllAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest createProductRequest)
    {
        var product = await _productsRepository.CreateAsync(createProductRequest.GetModel());
        return Ok(product);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Show([FromRoute] uint id)
    {
        var product = await _productsRepository.FindAsync(id);
        return Ok(product);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(
        [FromRoute] uint id,
        [FromBody] UpdateProductRequest updateProductRequest
    )
    {
        if (id != updateProductRequest.Id)
        {
            return BadRequest("Product ID mismatch.");
        }
        var result = await _productsRepository.UpdateAsync(id, updateProductRequest);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(uint id)
    {
        var result = await _productsRepository.DeleteAsync(id);
        return Ok(result);
    }
}
