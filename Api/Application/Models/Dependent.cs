namespace Api.Application.Models;

/// <summary>
/// Represents a dependent of an employee.
/// </summary>
public class Dependent
{
    /// <summary>
    /// Gets or sets the ID of the dependent.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the first name of the dependent.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the dependent.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the date of birth of the dependent.
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets the relationship of the dependent to the employee.
    /// </summary>
    public Relationship Relationship { get; set; }

    /// <summary>
    /// Gets or sets the ID of the employee to whom the dependent belongs.
    /// </summary>
    public int EmployeeId { get; set; }

    /// <summary>
    /// Gets or sets the employee to whom the dependent belongs.
    /// </summary>
    public Employee? Employee { get; set; }
}