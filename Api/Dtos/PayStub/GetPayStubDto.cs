namespace Api.Dtos.PayStub;

public record GetPayStubDto(
    int EmployeeId,
    decimal GrossPay,
    IEnumerable<DeductionDto> Deductions,
    decimal DeductionsTotal,
    decimal NetPay);