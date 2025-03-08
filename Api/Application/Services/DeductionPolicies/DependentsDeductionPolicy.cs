using Api.Application.Models;
using Api.Application.Services.Abstractions;

namespace Api.Application.Services.DeductionPolicies;

/// <summary>
/// Represents the deduction policy for dependents' benefits costs.
/// </summary>
public class DependentsDeductionPolicy : IDeductionPolicy
{
    private const decimal DependentMonthlyCost = 600;

    /// <summary>
    /// Gets the name of the deduction policy.
    /// </summary>
    public string Name => "Dependents Benefits Costs";

    /// <summary>
    /// Determines whether the deduction policy is applicable to the specified employee.
    /// </summary>
    /// <param name="employee">The employee to check.</param>
    /// <returns><c>true</c> if the employee has dependents; otherwise, <c>false</c>.</returns>
    public bool IsApplicable(Employee employee) => employee.Dependents.Count > 0;

    /// <summary>
    /// Calculates the deduction amount for the specified employee.
    /// </summary>
    /// <param name="employee">The employee for whom to calculate the deduction.</param>
    /// <param name="paychecksPerYear">The number of paychecks per year.</param>
    /// <returns>The calculated deduction amount.</returns>
    public decimal Calculate(Employee employee, int paychecksPerYear) =>
        IsApplicable(employee)
            ? employee.Dependents.Count * DependentMonthlyCost * 12 / paychecksPerYear
            : 0;
}