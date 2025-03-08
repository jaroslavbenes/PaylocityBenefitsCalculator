namespace Api.Application.Services.DeductionPolicies;

public record BaseDeductionPolicyOptions
{
    public const string Key = "BaseDeductionPolicy";

    public decimal BaseMonthlyCost { get; init; }
}