using Api.Services.Abstractions;

namespace Api.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddScoped<IDependentService, DependentService>()
            .AddScoped<IEmployeeService, EmployeeService>();
}