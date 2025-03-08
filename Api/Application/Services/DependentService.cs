using Api.Application.Models;
using Api.Application.Services.Abstractions;
using Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Api.Application.Services;

public class DependentService(ApplicationDbContext dbContext) : IDependentService
{
    public async Task<IEnumerable<Dependent>> GetAllDependents(CancellationToken ct = default) =>
        await dbContext.Dependents.ToListAsync(ct);

    public async Task<Dependent?> GetDependent(int id, CancellationToken ct = default) =>
        await dbContext.Dependents.FindAsync([id], ct);
}