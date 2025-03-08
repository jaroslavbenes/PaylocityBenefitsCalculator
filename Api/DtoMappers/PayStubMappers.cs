using Api.Dtos.PayStub;
using Api.Models;

namespace Api.DtoMappers;

public static class PayStubMappers
{
    public static GetPayStubDto ToGetPaycheckDto(this PayStub payStub) =>
        new(
            payStub.EmployeeId,
            payStub.GrossPay,
            payStub.Deductions.Select(d => new DeductionDto(d.Name, d.Amount)),
            payStub.DeductionsTotal,
            payStub.NetPay);
}