using Api.Models;
using Api.Persistence;
using Api.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class DependentService(ApplicationDbContext dbContext) : IDependentService
{
    public async Task<IEnumerable<Dependent>> GetAllDependents(CancellationToken ct = default) =>
        await dbContext.Dependents.ToListAsync(ct);

    public async Task<Dependent?> GetDependent(int id, CancellationToken ct = default) =>
        await dbContext.Dependents.FindAsync([id], ct);
}