using Api.Application.Models;

namespace Api.Application.Services.Abstractions;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllEmployees(CancellationToken ct = default);
    Task<Employee?> GetEmployee(int id, CancellationToken ct = default);
    Task<PayStub?> GetPayStub(int employeeId, CancellationToken ct);
}