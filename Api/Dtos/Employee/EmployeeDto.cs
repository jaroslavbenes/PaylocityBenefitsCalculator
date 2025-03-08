using Api.Dtos.Dependent;

namespace Api.Dtos.Employee;

public sealed record EmployeeDto(
    int Id,
    string? FirstName,
    string? LastName,
    decimal Salary,
    DateTime DateOfBirth,
    ICollection<DependentDto> Dependents);