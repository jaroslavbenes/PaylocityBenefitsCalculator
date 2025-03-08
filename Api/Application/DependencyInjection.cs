using Api.Application.Services;
using Api.Application.Services.Abstractions;
using Api.Application.Services.DeductionPolicies;

namespace Api.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
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