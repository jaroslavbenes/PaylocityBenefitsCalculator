using Microsoft.EntityFrameworkCore;

namespace Api.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services) =>
        services
            .AddDbContext<ApplicationDbContext>(
                (sp, options) =>
                {
                    var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("Database");
                    options.UseSqlite(connectionString);
                });
}