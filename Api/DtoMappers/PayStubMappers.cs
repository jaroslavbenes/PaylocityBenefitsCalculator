namespace Api.DtoMappers;

public static class PayStubMappers
{
    public static Dtos.PayStub.PayStubDto ToPayStubDto(this Application.Models.PayStub payStub) =>
        new(
            payStub.EmployeeId,
            payStub.GrossPay,
            payStub.Deductions.Select(d => new Dtos.PayStub.DeductionDto(d.Name, d.Amount)),
            payStub.DeductionsTotal,
            payStub.NetPay);
}