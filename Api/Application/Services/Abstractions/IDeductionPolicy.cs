using Api.Application.Models;

namespace Api.Application.Services.Abstractions;

/// <summary>
/// Defines the interface for a deduction policy.
/// </summary>
public interface IDeductionPolicy
{
    /// <summary>
    /// Gets the name of the deduction policy.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Determines whether the deduction policy is applicable to the specified employee.
    /// </summary>
    /// <param name="employee">The employee to check.</param>
    /// <returns><c>true</c> if the policy is applicable; otherwise, <c>false</c>.</returns>
    bool IsApplicable(Employee employee);

    /// <summary>
    /// Calculates the deduction amount for the specified employee.
    /// </summary>
    /// <param name="employee">The employee for whom to calculate the deduction.</param>
    /// <param name="paychecksPerYear">The number of paychecks per year.</param>
    /// <returns>The calculated deduction amount.</returns>
    decimal Calculate(Employee employee, int paychecksPerYear);
}