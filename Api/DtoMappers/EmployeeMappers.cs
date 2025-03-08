using Api.Dtos.Employee;
using Api.Models;

namespace Api.DtoMappers;

public static class EmployeeMappers
{
    public static GetEmployeeDto ToGetEmployeeDto(this Employee employee) =>
        new()
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Salary = employee.Salary,
            DateOfBirth = employee.DateOfBirth,
            Dependents = employee.Dependents.Select(d => d.ToGetDependentDto()).ToList()
        };
}