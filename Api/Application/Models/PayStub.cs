namespace Api.Application.Models;

/// <summary>
/// Represents a pay stub for an employee.
/// </summary>
/// <param name="EmployeeId">The ID of the employee.</param>
/// <param name="GrossPay">The gross pay of the employee.</param>
/// <param name="Deductions">The collection of deductions applied to the employee's pay.</param>
/// <param name="DeductionsTotal">The total amount of deductions.</param>
/// <param name="NetPay">The net pay of the employee after deductions.</param>
public record PayStub(
    int EmployeeId,
    decimal GrossPay,
    IEnumerable<Deduction> Deductions,
    decimal DeductionsTotal,
    decimal NetPay);