using Api.Application.Models;
using Api.Application.Services.Abstractions;

namespace Api.Application.Services.DeductionPolicies;

public class BaseDeductionPolicy : IDeductionPolicy
{
    private const decimal BaseMonthlyCost = 1_000;

    public string Name => "Base Benefits Costs";

    public bool IsApplicable(Employee employee) => true;

    public decimal Calculate(Employee employee, int paychecksPerYear) =>
        IsApplicable(employee)
            ? BaseMonthlyCost * 12 / paychecksPerYear
            : 0;
}