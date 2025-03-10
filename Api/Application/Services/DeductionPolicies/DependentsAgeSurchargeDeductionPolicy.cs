using Api.Application.Models;
using Api.Application.Services.Abstractions;
using Microsoft.Extensions.Options;

namespace Api.Application.Services.DeductionPolicies;

/// <summary>
/// Represents the deduction policy that applies a surcharge based on the age of dependents.
/// </summary>
public class DependentsAgeSurchargeDeductionPolicy(IOptions<DependentsAgeSurchargeDeductionPolicyOptions> options) : IDeductionPolicy
{
    private readonly DependentsAgeSurchargeDeductionPolicyOptions _options = options.Value;

    /// <summary>
    /// Gets the name of the deduction policy.
    /// </summary>
    public string Name => "Dependents age surcharge";

    /// <summary>
    /// Determines whether the deduction policy is applicable to the specified employee.
    /// </summary>
    /// <param name="employee">The employee to check.</param>
    /// <returns><c>true</c> if any dependent is older than the age threshold; otherwise, <c>false</c>.</returns>
    public bool IsApplicable(Employee employee) =>
        employee.Dependents.Any(d => d.DateOfBirth.AddYears(_options.AgeThreshold) <= DateTime.Today);

    /// <summary>
    /// Calculates the deduction amount for the specified employee.
    /// </summary>
    /// <param name="employee">The employee for whom to calculate the deduction.</param>
    /// <param name="paychecksPerYear">The number of paychecks per year.</param>
    /// <returns>The calculated deduction amount.</returns>
    public decimal Calculate(Employee employee, int paychecksPerYear) =>
        IsApplicable(employee)
            ? employee.Dependents.Count(d => d.DateOfBirth.AddYears(_options.AgeThreshold) <= DateTime.Today) * _options.MonthlySurchargePerDependent * 12 / paychecksPerYear
            : 0;
}