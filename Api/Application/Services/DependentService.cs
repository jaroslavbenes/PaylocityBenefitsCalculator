using Api.Application.Models;
using Api.Application.Services.Abstractions;
using Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Application.Services;

/// <summary>
/// Provides methods handling the use-cases related to dependents.
/// </summary>
public class DependentService(ApplicationDbContext dbContext) : IDependentService
{
    private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    /// <summary>
    /// Gets all dependents.
    /// </summary>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of dependents.</returns>
    public async Task<IEnumerable<Dependent>> GetAllDependents(CancellationToken ct = default) =>
        await _dbContext.Dependents.ToListAsync(ct);

    /// <summary>
    /// Gets a dependent by ID.
    /// </summary>
    /// <param name="id">The ID of the dependent.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the dependent if found; otherwise, null.</returns>
    public async Task<Dependent?> GetDependent(int id, CancellationToken ct = default) =>
        await _dbContext.Dependents.FindAsync([id], ct);
}