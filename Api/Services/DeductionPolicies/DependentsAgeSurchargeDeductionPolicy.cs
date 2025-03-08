using Api.Models;
using Api.Services.Abstractions;

namespace Api.Services.DeductionPolicies;

public class DependentsAgeSurchargeDeductionPolicy : IDeductionPolicy
{
    private const int AgeThreshold = 50;
    private const decimal MonthlySurchargePerDependent = 200;

    public string Name => "Dependents age surcharge";

    public bool IsApplicable(Employee employee) => employee.Dependents.Any(d => d.DateOfBirth.AddYears(AgeThreshold) <= DateTime.Today);

    public decimal Calculate(Employee employee, int paychecksPerYear) =>
        employee.Dependents.Count(d => d.DateOfBirth.AddYears(AgeThreshold) <= DateTime.Today) * MonthlySurchargePerDependent * 12 / paychecksPerYear;
}