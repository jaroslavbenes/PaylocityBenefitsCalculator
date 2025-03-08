namespace Api.DtoMappers;

public static class EmployeeMappers
{
    public static Dtos.Employee.EmployeeDto ToEmployeeDto(this Application.Models.Employee employee) =>
        new(
            Id: employee.Id,
            FirstName: employee.FirstName,
            LastName: employee.LastName,
            Salary: employee.Salary,
            DateOfBirth: employee.DateOfBirth,
            Dependents: employee.Dependents.Select(d => d.ToDependentDto()).ToList());
}