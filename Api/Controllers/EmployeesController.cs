using Api.DtoMappers;
using Api.Dtos.Employee;
using Api.Dtos.PayStub;
using Api.Models;
using Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController(IEmployeeService service) : ControllerBase
{
    [SwaggerOperation(Summary = "Get employee by id")]
    [ProducesResponseType(200, Type = typeof(ApiResponse<GetEmployeeDto>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse<GetEmployeeDto>))]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id, CancellationToken ct)
    {
        var employee = await service.GetEmployee(id, ct);

        return employee is null
            ? NotFound(new ApiResponse<GetEmployeeDto> { Message = "Employee not found" })
            : Ok(new ApiResponse<GetEmployeeDto> { Data = employee.ToGetEmployeeDto(), Success = true });
    }

    [SwaggerOperation(Summary = "Calculate employee's pay stub")]
    [ProducesResponseType(200, Type = typeof(ApiResponse<GetPayStubDto>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse<GetPayStubDto>))]
    [HttpGet("{id:int}/PayStub")]
    public async Task<ActionResult<ApiResponse<GetPayStubDto>>> GetPayStub(int id, CancellationToken ct)
    {
        var payStub = await service.GetPayStub(id, ct);

        return payStub is null
            ? NotFound(new ApiResponse<GetPayStubDto> { Message = "Employee not found" })
            : Ok(new ApiResponse<GetPayStubDto> { Data = payStub.ToGetPaycheckDto(), Success = true });
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [ProducesResponseType(200, Type = typeof(ApiResponse<List<GetEmployeeDto>>))]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll(CancellationToken ct)
    {
        var employees = await service.GetAllEmployees(ct);
        var data = employees.Select(e => e.ToGetEmployeeDto()).ToList();
        return Ok(new ApiResponse<List<GetEmployeeDto>> { Data = data, Success = true });
    }
}