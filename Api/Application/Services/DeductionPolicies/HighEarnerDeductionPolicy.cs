using Api.Application.Models;
using Api.Application.Services.Abstractions;
using Microsoft.Extensions.Options;

namespace Api.Application.Services.DeductionPolicies;

/// <summary>
/// Represents the deduction policy that applies a surcharge for high earners.
/// </summary>
public class HighEarnerDeductionPolicy(IOptions<HighEarnerDeductionPolicyOptions> options) : IDeductionPolicy
{
    private readonly HighEarnerDeductionPolicyOptions _options = options.Value;

    /// <summary>
    /// Gets the name of the deduction policy.
    /// </summary>
    public string Name => "High Earner 2% Surcharge";

    /// <summary>
    /// Determines whether the deduction policy is applicable to the specified employee.
    /// </summary>
    /// <param name="employee">The employee to check.</param>
    /// <returns><c>true</c> if the employee's salary is above the salary threshold; otherwise, <c>false</c>.</returns>
    public bool IsApplicable(Employee employee) => employee.Salary > _options.SalaryThreshold;

    /// <summary>
    /// Calculates the deduction amount for the specified employee.
    /// </summary>
    /// <param name="employee">The employee for whom to calculate the deduction.</param>
    /// <param name="paychecksPerYear">The number of paychecks per year.</param>
    /// <returns>The calculated deduction amount.</returns>
    public decimal Calculate(Employee employee, int paychecksPerYear) =>
        IsApplicable(employee)
            ? employee.Salary * _options.SurchargeRate / paychecksPerYear
            : 0;
}