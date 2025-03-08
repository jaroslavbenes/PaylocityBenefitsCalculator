using Api.Models;

namespace Api.Services.Abstractions;

public interface IDependentService
{
    Task<IEnumerable<Dependent>> GetAllDependents(CancellationToken ct = default);
    Task<Dependent?> GetDependent(int id, CancellationToken ct = default);
}