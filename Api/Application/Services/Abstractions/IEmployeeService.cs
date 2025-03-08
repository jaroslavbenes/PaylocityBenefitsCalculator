using Api.Application.Models;

namespace Api.Application.Services.Abstractions;

/// <summary>
/// Defines the interface for an employee service.
/// </summary>
public interface IEmployeeService
{
    /// <summary>
    /// Gets all employees.
    /// </summary>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of employees.</returns>
    Task<IEnumerable<Employee>> GetAllEmployees(CancellationToken ct = default);

    /// <summary>
    /// Gets an employee by ID.
    /// </summary>
    /// <param name="id">The ID of the employee.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the employee if found; otherwise, null.</returns>
    Task<Employee?> GetEmployee(int id, CancellationToken ct = default);

    /// <summary>
    /// Gets the pay stub for an employee by ID.
    /// </summary>
    /// <param name="employeeId">The ID of the employee.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the pay stub if found; otherwise, null.</returns>
    Task<PayStub?> GetPayStub(int employeeId, CancellationToken ct);
}