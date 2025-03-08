using Api.Application.Models;
using Api.Application.Services.Abstractions;

namespace Api.Application.Services;

public class PayStubService(IEnumerable<IDeductionPolicy> deductionPolicies) : IPayStubService
{
    private readonly IEnumerable<IDeductionPolicy> _deductionPolicies = deductionPolicies ?? throw new ArgumentNullException(nameof(deductionPolicies));
    private const int PaychecksPerYear = 26;

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