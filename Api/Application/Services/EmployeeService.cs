using Api.Application.Models;
using Api.Application.Services.Abstractions;
using Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Application.Services;

public class EmployeeService(ApplicationDbContext dbContext, IPayStubService payStubService) : IEmployeeService
{
    private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    private readonly IPayStubService _payStubService = payStubService ?? throw new ArgumentNullException(nameof(payStubService));

    public async Task<IEnumerable<Employee>> GetAllEmployees(CancellationToken ct = default) =>
        await
            _dbContext.Employees
                .Include(e => e.Dependents)
                .ToListAsync(ct);

    public async Task<Employee?> GetEmployee(int id, CancellationToken ct = default) =>
        await
            _dbContext.Employees
                .Include(e => e.Dependents)
                .FirstOrDefaultAsync(e => e.Id == id, ct);

    public async Task<PayStub?> GetPayStub(int employeeId, CancellationToken ct)
    {
        var employee = await GetEmployee(employeeId, ct);
        return employee is null ? null : _payStubService.GetPayStub(employee);
    }
}