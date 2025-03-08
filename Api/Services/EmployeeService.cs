using Api.Models;
using Api.Persistence;
using Api.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class EmployeeService(ApplicationDbContext dbContext) : IEmployeeService
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
}