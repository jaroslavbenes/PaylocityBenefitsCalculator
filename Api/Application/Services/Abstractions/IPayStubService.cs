using Api.Application.Models;

namespace Api.Application.Services.Abstractions;

/// <summary>
/// Defines the interface for a pay stub service.
/// </summary>
public interface IPayStubService
{
    /// <summary>
    /// Gets the pay stub for the specified employee.
    /// </summary>
    /// <param name="employee">The employee for whom to get the pay stub.</param>
    /// <returns>The pay stub for the specified employee.</returns>
    PayStub GetPayStub(Employee employee);
}