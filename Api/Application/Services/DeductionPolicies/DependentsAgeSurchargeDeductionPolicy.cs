using Api.Application.Models;
using Api.Application.Services.Abstractions;

namespace Api.Application.Services.DeductionPolicies;

public class DependentsAgeSurchargeDeductionPolicy : IDeductionPolicy
{
    private const int AgeThreshold = 50;
    private const decimal MonthlySurchargePerDependent = 200;

    public string Name => "Dependents age surcharge";

    public bool IsApplicable(Employee employee) => employee.Dependents.Any(d => d.DateOfBirth.AddYears(AgeThreshold) <= DateTime.Today);

    public decimal Calculate(Employee employee, int paychecksPerYear) =>
        IsApplicable(employee)
            ? employee.Dependents.Count(d => d.DateOfBirth.AddYears(AgeThreshold) <= DateTime.Today) * MonthlySurchargePerDependent * 12 / paychecksPerYear
            : 0;
}