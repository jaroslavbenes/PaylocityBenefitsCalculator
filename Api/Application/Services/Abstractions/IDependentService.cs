using Api.Application.Models;

namespace Api.Application.Services.Abstractions;

public interface IDependentService
{
    Task<IEnumerable<Dependent>> GetAllDependents(CancellationToken ct = default);
    Task<Dependent?> GetDependent(int id, CancellationToken ct = default);
}