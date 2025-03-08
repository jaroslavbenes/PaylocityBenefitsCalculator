using Api.Application.Models;
using Api.Application.Services.Abstractions;

namespace Api.Application.Services.DeductionPolicies;

/// <summary>
/// Represents the deduction policy that applies a surcharge for high earners.
/// </summary>
public class HighEarnerDeductionPolicy : IDeductionPolicy
{
    private const decimal SalaryThreshold = 80_000;
    private const decimal SurchargeRate = 0.02m;

    /// <summary>
    /// Gets the name of the deduction policy.
    /// </summary>
    public string Name => "High Earner 2% Surcharge";

    /// <summary>
    /// Determines whether the deduction policy is applicable to the specified employee.
    /// </summary>
    /// <param name="employee">The employee to check.</param>
    /// <returns><c>true</c> if the employee's salary is above the salary threshold; otherwise, <c>false</c>.</returns>
    public bool IsApplicable(Employee employee) => employee.Salary > SalaryThreshold;

    /// <summary>
    /// Calculates the deduction amount for the specified employee.
    /// </summary>
    /// <param name="employee">The employee for whom to calculate the deduction.</param>
    /// <param name="paychecksPerYear">The number of paychecks per year.</param>
    /// <returns>The calculated deduction amount.</returns>
    public decimal Calculate(Employee employee, int paychecksPerYear) =>
        IsApplicable(employee)
            ? employee.Salary * SurchargeRate / paychecksPerYear
            : 0;
}