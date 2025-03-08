namespace Api.Application.Models;

/// <summary>
/// Represents the relationship of a dependent to an employee.
/// </summary>
public enum Relationship
{
    /// <summary>
    /// No relationship.
    /// </summary>
    None,

    /// <summary>
    /// Spouse of the employee.
    /// </summary>
    Spouse,

    /// <summary>
    /// Domestic partner of the employee.
    /// </summary>
    DomesticPartner,

    /// <summary>
    /// Child of the employee.
    /// </summary>
    Child
}