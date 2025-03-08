using Api.Application.Services.Abstractions;
using Api.DtoMappers;
using Api.Dtos;
using Api.Dtos.Employee;
using Api.Dtos.PayStub;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController(IEmployeeService service) : ControllerBase
{
    private readonly IEmployeeService _service = service ?? throw new ArgumentNullException(nameof(service));

    [SwaggerOperation(Summary = "Get employee by id")]
    [ProducesResponseType(200, Type = typeof(ApiResponse<EmployeeDto>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse<EmployeeDto>))]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<EmployeeDto>>> Get(int id, CancellationToken ct)
    {
        var employee = await _service.GetEmployee(id, ct);

        return employee is null
            ? NotFound(new ApiResponse<EmployeeDto>(Data: null, Success: false, Message: "Employee not found"))
            : Ok(new ApiResponse<EmployeeDto>(Data: employee.ToEmployeeDto(), Success: true));
    }

    [SwaggerOperation(Summary = "Calculate employee's pay stub")]
    [ProducesResponseType(200, Type = typeof(ApiResponse<PayStubDto>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse<PayStubDto>))]
    [HttpGet("{id:int}/PayStub")]
    public async Task<ActionResult<ApiResponse<PayStubDto>>> GetPayStub(int id, CancellationToken ct)
    {
        var payStub = await _service.GetPayStub(id, ct);

        return payStub is null
            ? NotFound(new ApiResponse<PayStubDto>(Data: null, Success: false, Message: "Employee not found"))
            : Ok(new ApiResponse<PayStubDto>(Data: payStub.ToPayStubDto(), Success: true));
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [ProducesResponseType(200, Type = typeof(ApiResponse<List<EmployeeDto>>))]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<EmployeeDto>>>> GetAll(CancellationToken ct)
    {
        var employees = await _service.GetAllEmployees(ct);
        var data = employees.Select(e => e.ToEmployeeDto()).ToList();
        return Ok(new ApiResponse<List<EmployeeDto>>(Data: data, Success: true));
    }
}