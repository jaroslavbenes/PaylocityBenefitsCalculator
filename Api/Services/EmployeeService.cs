using Api.Models;
using Api.Persistence;
using Api.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class EmployeeService(ApplicationDbContext dbContext, IPayStubService payStubService) : IEmployeeService
{
    public async Task<IEnumerable<Employee>> GetAllEmployees(CancellationToken ct = default) =>
        await
            dbContext.Employees
                .Include(e => e.Dependents)
                .ToListAsync(ct);

    public async Task<Employee?> GetEmployee(int id, CancellationToken ct = default) =>
        await
            dbContext.Employees
                .Include(e => e.Dependents)
                .FirstOrDefaultAsync(e => e.Id == id, ct);

    public async Task<PayStub?> GetPayStub(int employeeId, CancellationToken ct)
    {
        var employee = await GetEmployee(employeeId, ct);
        return employee is null ? null : payStubService.GetPayStub(employee);
    }
}