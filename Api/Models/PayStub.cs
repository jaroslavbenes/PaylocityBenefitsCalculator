namespace Api.Models;

public record PayStub(
    int EmployeeId,
    decimal GrossPay,
    IEnumerable<Deduction> Deductions,
    decimal DeductionsTotal,
    decimal NetPay);