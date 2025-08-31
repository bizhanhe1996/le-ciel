using LeCiel.Database.Repositories;
using LeCiel.DTOs.Requests.Product;
using Microsoft.AspNetCore.Mvc;

namespace LeCiel.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    public async Task<IActionResult> CreateAsync(CreateProductRequest createProductRequest)
    {
        var product = await _productsRepository.CreateAsync(createProductRequest.GetModel());
        return Ok(product);
    }
}
