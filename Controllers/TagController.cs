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
    public async Task<IActionResult> Create(TagCreateRequestDto dto)
    {
        var insertedTag = await _tagsRepository.CreateAsync(dto.GetModel());
        var response = new GenericResponse<TagResponseDto>(
            insertedTag != null,
            insertedTag?.GetDto()
        );
        return Ok(response);
    }
}
