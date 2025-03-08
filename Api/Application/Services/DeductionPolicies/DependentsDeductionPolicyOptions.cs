namespace Api.Application.Services.DeductionPolicies;

public record DependentsDeductionPolicyOptions
{
    public const string Key = "DependentsDeductionPolicy";

    public decimal DependentMonthlyCost { get; init; }
}