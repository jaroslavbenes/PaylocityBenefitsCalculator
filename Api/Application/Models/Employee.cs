namespace Api.Application.Models;

/// <summary>
/// Represents an employee.
/// </summary>
public class Employee
{
    /// <summary>
    /// Gets or sets the ID of the employee.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the first name of the employee.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the employee.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the salary of the employee.
    /// </summary>
    public decimal Salary { get; set; }

    /// <summary>
    /// Gets or sets the date of birth of the employee.
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets the collection of dependents of the employee.
    /// </summary>
    public ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();
}