using Api.Models;
using Api.Services.Abstractions;

namespace Api.Services.DeductionPolicies;

public class HighEarnerDeductionPolicy : IDeductionPolicy
{
    private const decimal SalaryThreshold = 80_000;
    private const decimal SurchargeRate = 0.02m;

    public string Name => "High Earner 2% Surcharge";

    public bool IsApplicable(Employee employee) => employee.Salary > SalaryThreshold;
    public decimal Calculate(Employee employee, int paychecksPerYear) => employee.Salary * SurchargeRate / paychecksPerYear;
}