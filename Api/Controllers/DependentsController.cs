using Api.Application.Services.Abstractions;
using Api.DtoMappers;
using Api.Dtos;
using Api.Dtos.Dependent;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController(IDependentService service) : ControllerBase
{
    private readonly IDependentService _service = service ?? throw new ArgumentNullException(nameof(service));

    [SwaggerOperation(Summary = "Get dependent by id")]
    [ProducesResponseType(200, Type = typeof(ApiResponse<DependentDto>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse<DependentDto>))]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<DependentDto>>> Get(int id, CancellationToken ct)
    {
        var dependent = await _service.GetDependent(id, ct);
        return dependent is null
            ? NotFound(new ApiResponse<DependentDto>(Data: null, Success: false, Message: "Dependent not found"))
            : Ok(new ApiResponse<DependentDto>(Data: dependent.ToDependentDto(), Success: true));
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [ProducesResponseType(200, Type = typeof(ApiResponse<List<DependentDto>>))]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<DependentDto>>>> GetAll(CancellationToken ct)
    {
        var dependents = await _service.GetAllDependents(ct);
        var data = dependents.Select(d => d.ToDependentDto()).ToList();
        return Ok(new ApiResponse<List<DependentDto>>(Data: data, Success: true));
    }
}