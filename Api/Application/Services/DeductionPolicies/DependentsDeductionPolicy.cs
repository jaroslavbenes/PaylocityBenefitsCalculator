using Api.Application.Models;
using Api.Application.Services.Abstractions;

namespace Api.Application.Services.DeductionPolicies;

public class DependentsDeductionPolicy : IDeductionPolicy
{
    private const decimal DependentMonthlyCost = 600;

    public string Name => "Dependents Benefits Costs";

    public bool IsApplicable(Employee employee) => employee.Dependents.Count > 0;

    public decimal Calculate(Employee employee, int paychecksPerYear) =>
        IsApplicable(employee)
            ? employee.Dependents.Count * DependentMonthlyCost * 12 / paychecksPerYear
            : 0;
}