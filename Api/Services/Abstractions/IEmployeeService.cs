using Api.Models;

namespace Api.Services.Abstractions;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllEmployees(CancellationToken ct = default);
    Task<Employee?> GetEmployee(int id, CancellationToken ct = default);
}