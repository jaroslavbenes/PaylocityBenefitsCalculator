using Api.Models;
using Api.Services.Abstractions;

namespace Api.Services;

public class PayStubService(IEnumerable<IDeductionPolicy> deductionPolicies) : IPayStubService
{
    private const int PaychecksPerYear = 26;

    public PayStub GetPayStub(Employee employee)
    {
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