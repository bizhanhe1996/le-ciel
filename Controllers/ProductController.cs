using LeCiel.Database.Repositories;
using LeCiel.DTOs.Requests.Product;
using Microsoft.AspNetCore.Mvc;

namespace LeCiel.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(ProductsRepository productsRepository) : BaseController
{
    private readonly ProductsRepository _productsRepository = productsRepository;

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateProductRequest createProductRequest)
    {
        var product = await _productsRepository.CreateAsync(createProductRequest.GetModel());
        return Ok(product);
    }
}
