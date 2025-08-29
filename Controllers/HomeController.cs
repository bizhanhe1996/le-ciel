using Microsoft.AspNetCore.Mvc;

namespace LeCiel.Controllers;

[ApiController, Route("api")]
public class HomeController : BaseController
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Bienvenue au Ciel!");
    }
}
