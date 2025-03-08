using Api.DtoMappers;
using Api.Dtos.Dependent;
using Api.Models;
using Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController(IDependentService service) : ControllerBase
{
    [SwaggerOperation(Summary = "Get dependent by id")]
    [ProducesResponseType(200, Type = typeof(ApiResponse<GetDependentDto>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse<GetDependentDto>))]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id, CancellationToken ct)
    {
        var dependent = await service.GetDependent(id, ct);
        return dependent is null
            ? NotFound(new ApiResponse<GetDependentDto> { Message = "Dependent not found" })
            : Ok(new ApiResponse<GetDependentDto> { Data = dependent.ToGetDependentDto(), Success = true });
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [ProducesResponseType(200, Type = typeof(ApiResponse<List<GetDependentDto>>))]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll(CancellationToken ct)
    {
        var dependents = await service.GetAllDependents(ct);
        var data = dependents.Select(d => d.ToGetDependentDto()).ToList();
        return Ok(new ApiResponse<List<GetDependentDto>> { Data = data, Success = true });
    }
}
