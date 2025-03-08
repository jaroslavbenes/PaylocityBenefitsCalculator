using Api.Application.Models;
using Api.Application.Services.Abstractions;
using Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Application.Services;

/// <summary>
/// Provides methods for handling the use-cases related to employees.
/// </summary>
public class EmployeeService(ApplicationDbContext dbContext, IPayStubService payStubService) : IEmployeeService
{
    private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    private readonly IPayStubService _payStubService = payStubService ?? throw new ArgumentNullException(nameof(payStubService));

    /// <summary>
    /// Gets all employees.
    /// </summary>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of employees.</returns>
    public async Task<IEnumerable<Employee>> GetAllEmployees(CancellationToken ct = default) =>
        await
            _dbContext.Employees
                .Include(e => e.Dependents)
                .ToListAsync(ct);

    /// <summary>
    /// Gets an employee by ID.
    /// </summary>
    /// <param name="id">The ID of the employee.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the employee if found; otherwise, null.</returns>
    public async Task<Employee?> GetEmployee(int id, CancellationToken ct = default) =>
        await
            _dbContext.Employees
                .Include(e => e.Dependents)
                .FirstOrDefaultAsync(e => e.Id == id, ct);

    /// <summary>
    /// Gets the pay stub for an employee by ID.
    /// </summary>
    /// <param name="employeeId">The ID of the employee.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the pay stub if found; otherwise, null.</returns>
    public async Task<PayStub?> GetPayStub(int employeeId, CancellationToken ct)
    {
        var employee = await GetEmployee(employeeId, ct);
        return employee is null ? null : _payStubService.GetPayStub(employee);
    }
}