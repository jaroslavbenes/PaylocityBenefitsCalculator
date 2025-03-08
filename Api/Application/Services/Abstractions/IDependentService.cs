using Api.Application.Models;

namespace Api.Application.Services.Abstractions;

/// <summary>
/// Defines the interface for a dependent service.
/// </summary>
public interface IDependentService
{
    /// <summary>
    /// Gets all dependents.
    /// </summary>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of dependents.</returns>
    Task<IEnumerable<Dependent>> GetAllDependents(CancellationToken ct = default);

    /// <summary>
    /// Gets a dependent by ID.
    /// </summary>
    /// <param name="id">The ID of the dependent.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the dependent if found; otherwise, null.</returns>
    Task<Dependent?> GetDependent(int id, CancellationToken ct = default);
}