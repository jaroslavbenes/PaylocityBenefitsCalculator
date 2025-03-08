using Api.Models;
using Api.Persistence;
using Api.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class EmployeeService(ApplicationDbContext dbContext, IEnumerable<IDeductionPolicy> deductionPolicies) : IEmployeeService
{
    private const int PaychecksPerYear = 26;

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

        if (employee is null)
        {
            return null;
        }

        var grossPay = Math.Round(employee.Salary / PaychecksPerYear, 2);
        var applicableDeductions = deductionPolicies.Where(rule => rule.IsApplicable(employee));

        var deductions =
            applicableDeductions
                .Select(rule => new Deduction(rule.Name, Math.Round(rule.Calculate(employee, PaychecksPerYear), 2)))
                .ToList();

        var deductionsTotal = Math.Round(deductions.Sum(d => d.Amount), 2);
        var netPay = Math.Round(grossPay - deductionsTotal, 2);

        return
            new PayStub(
                employee.Id,
                grossPay,
                deductions,
                deductionsTotal,
                netPay);
    }
}