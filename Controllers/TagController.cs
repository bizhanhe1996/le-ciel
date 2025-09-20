using LeCiel.Database.Repositories;
using LeCiel.DTOs.Requests;
using LeCiel.DTOs.Responses;
using LeCiel.Extras.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LeCiel.Controllers;

[ApiController, Route("api/[controller]")]
public class TagController : BaseController, IController<TagCreateRequestDto, TagUpdateRequestDto>
{
    private readonly TagsRepository _tagsRepository;

    public TagController(TagsRepository tagsRepository)
    {
        _tagsRepository = tagsRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TagCreateRequestDto dto)
    {
        var insertedTag = await _tagsRepository.CreateAsync(dto.GetModel());
        var response = new GenericResponse<TagResponseFullDto?>(
            insertedTag is not null,
            insertedTag?.GetFullDto()
        );
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var tags = await _tagsRepository.IndexAsync(page, pageSize);
        var response = new GenericResponse<List<TagResponseSimpleDto>?>(
            true,
            [.. tags.Select(t => t.GetSimpleDto())],
            _tagsRepository.PaginationStruct
        );
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Find([FromRoute] uint id)
    {
        var result = await _tagsRepository.FindAsync(id);
        var response = new GenericResponse<TagResponseFullDto?>(
            result is not null,
            result?.GetFullDto()
        );
        return Ok(response);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update([FromRoute] uint id, [FromBody] TagUpdateRequestDto dto)
    {
        var result = await _tagsRepository.UpdateAsync(id, dto);
        var response = new GenericResponse<TagResponseFullDto?>(
            result is not null,
            result?.GetFullDto()
        );
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] uint id)
    {
        var result = await _tagsRepository.DeleteAsync(id);
        var response = new GenericResponse<TagResponseFullDto?>(
            result is not null,
            result?.GetFullDto()
        );
        return Ok(response);
    }

    [HttpGet("/products/{id}")]
    public async Task<IActionResult> Products([FromRoute] uint id)
    {
        var products = await _tagsRepository.ProductsAsync(id);
        var response = new GenericResponse<List<ProductResponseSimpleDto>?>(
            products is not null,
            products?.Select(p => p.GetSimpleDto()).ToList()
        );
        return Ok(response);
    }
}
