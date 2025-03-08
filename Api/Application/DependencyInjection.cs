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

    private static IServiceCollection AddDeductionPolicies(this IServiceCollection services)
    {
        services
            .AddScoped<IDeductionPolicy, BaseDeductionPolicy>()
            .AddOptions<BaseDeductionPolicyOptions>()
            .Configure<IConfiguration>((o, c) => { c.GetSection(BaseDeductionPolicyOptions.Key).Bind(o); })
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddScoped<IDeductionPolicy, DependentsDeductionPolicy>()
            .AddOptions<DependentsDeductionPolicyOptions>()
            .Configure<IConfiguration>((o, c) => { c.GetSection(DependentsDeductionPolicyOptions.Key).Bind(o); })
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddScoped<IDeductionPolicy, HighEarnerDeductionPolicy>()
            .AddOptions<HighEarnerDeductionPolicyOptions>()
            .Configure<IConfiguration>((o, c) => { c.GetSection(HighEarnerDeductionPolicyOptions.Key).Bind(o); })
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddScoped<IDeductionPolicy, DependentsAgeSurchargeDeductionPolicy>()
            .AddOptions<DependentsAgeSurchargeDeductionPolicyOptions>()
            .Configure<IConfiguration>((o, c) => { c.GetSection(DependentsAgeSurchargeDeductionPolicyOptions.Key).Bind(o); })
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}