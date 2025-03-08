namespace Api.Application.Models;

/// <summary>
/// Represents a deduction applied to an employee's pay.
/// </summary>
/// <param name="Name">The name of the deduction.</param>
/// <param name="Amount">The amount of the deduction.</param>
public record Deduction(string Name, decimal Amount);