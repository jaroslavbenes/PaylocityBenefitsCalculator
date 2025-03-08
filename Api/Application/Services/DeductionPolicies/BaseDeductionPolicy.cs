using Api.Application.Models;
using Api.Application.Services.Abstractions;
using Microsoft.Extensions.Options;

namespace Api.Application.Services.DeductionPolicies;

/// <summary>
/// Represents the base costs for benefits deduction for employee benefits.
/// </summary>
public class BaseDeductionPolicy(IOptions<BaseDeductionPolicyOptions> options) : IDeductionPolicy
{
    private readonly BaseDeductionPolicyOptions _options = options.Value;

    /// <summary>
    /// Gets the name of the deduction policy.
    /// </summary>
    public string Name => "Base Benefits Costs";

    /// <summary>
    /// Determines whether the deduction policy is applicable to the specified employee.
    /// </summary>
    /// <param name="employee">The employee to check.</param>
    /// <returns><c>true</c> since all employees are eligible for the base benefits costs.</returns>
    public bool IsApplicable(Employee employee) => true;

    /// <summary>
    /// Calculates the deduction amount for the specified employee based on the base monthly cost.
    /// </summary>
    /// <param name="employee">The employee for whom to calculate the deduction.</param>
    /// <param name="paychecksPerYear">The number of paychecks per year.</param>
    /// <returns>The calculated deduction amount.</returns>
    public decimal Calculate(Employee employee, int paychecksPerYear) =>
        IsApplicable(employee)
            ? _options.BaseMonthlyCost * 12 / paychecksPerYear
            : 0;
}