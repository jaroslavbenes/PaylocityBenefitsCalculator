using Api.Application.Models;

namespace Api.Application.Services.Abstractions;

public interface IDeductionPolicy
{
    string Name { get; }

    bool IsApplicable(Employee employee);
    decimal Calculate(Employee employee, int paychecksPerYear);
}