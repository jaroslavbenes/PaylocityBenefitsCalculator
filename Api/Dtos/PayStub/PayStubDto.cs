namespace Api.Dtos.PayStub;

public sealed record PayStubDto(
    int EmployeeId,
    decimal GrossPay,
    IEnumerable<DeductionDto> Deductions,
    decimal DeductionsTotal,
    decimal NetPay);