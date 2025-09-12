using LeCiel.Database.Repositories;
using LeCiel.DTOs.Requests;
using LeCiel.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LeCiel.Controllers;

[ApiController, Route("api/[controller]")]
public class TagController(TagsRepository tagsRepository) : BaseController
{
    private readonly TagsRepository _tagsRepository = tagsRepository;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TagCreateRequestDto dto)
    {
        var insertedTag = await _tagsRepository.CreateAsync(dto.GetModel());
        var response = new GenericResponse<TagResponseDto>(
            insertedTag != null,
            insertedTag?.GetDto()
        );
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var tags = await _tagsRepository.IndexAsync();
        var response = new GenericResponse<List<TagResponseDto>?>(
            true,
            [.. tags.Select(t => t.GetDto())]
        );
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Find([FromRoute] uint id)
    {
        var result = await _tagsRepository.FindAsync(id);
        var response = new GenericResponse<TagResponseDto?>(result != null, result?.GetDto());
        return Ok(response);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update([FromRoute] uint id, [FromBody] TagUpdateRequestDto dto)
    {
        var result = await _tagsRepository.UpdateAsync(id, dto);
        var response = new GenericResponse<TagResponseDto?>(result != null, result?.GetDto());
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(uint id)
    {
        var result = await _tagsRepository.DeleteAsync(id);
        var response = new GenericResponse<TagResponseDto?>(result != null, result?.GetDto());
        return Ok(response);
    }
}
