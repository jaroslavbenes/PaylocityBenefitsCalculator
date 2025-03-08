using Api.Models;

namespace Api.Services.Abstractions;

public interface IDeductionPolicy
{
    string Name { get; }

    bool IsApplicable(Employee employee);
    decimal Calculate(Employee employee, int paychecksPerYear);
}