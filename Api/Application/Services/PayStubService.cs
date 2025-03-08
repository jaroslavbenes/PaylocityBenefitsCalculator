using Api.Application.Models;
using Api.Application.Services.Abstractions;

namespace Api.Application.Services;

/// <summary>
/// Provides services for generating pay stubs.
/// </summary>
public class PayStubService(IEnumerable<IDeductionPolicy> deductionPolicies) : IPayStubService
{
    private readonly IEnumerable<IDeductionPolicy> _deductionPolicies = deductionPolicies ?? throw new ArgumentNullException(nameof(deductionPolicies));
    private const int PaychecksPerYear = 26;

    /// <summary>
    /// Generates a pay stub for the specified employee.
    /// </summary>
    /// <param name="employee">The employee for whom to generate the pay stub.</param>
    /// <returns>The generated pay stub.</returns>
    public PayStub GetPayStub(Employee employee)
    {
        var grossPay = Math.Round(employee.Salary / PaychecksPerYear, 2);
        var applicableDeductions = _deductionPolicies.Where(rule => rule.IsApplicable(employee));

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