using Api.Services.Abstractions;
using Api.Services.DeductionPolicies;

namespace Api.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddScoped<IDependentService, DependentService>()
            .AddScoped<IEmployeeService, EmployeeService>()
            .AddScoped<IPayStubService, PayStubService>()
            .AddDeductionPolicies();

    private static IServiceCollection AddDeductionPolicies(this IServiceCollection services) =>
        services
            .AddScoped<IDeductionPolicy, BaseDeductionPolicy>()
            .AddScoped<IDeductionPolicy, DependentsDeductionPolicy>()
            .AddScoped<IDeductionPolicy, HighEarnerDeductionPolicy>()
            .AddScoped<IDeductionPolicy, DependentsAgeSurchargeDeductionPolicy>();
}