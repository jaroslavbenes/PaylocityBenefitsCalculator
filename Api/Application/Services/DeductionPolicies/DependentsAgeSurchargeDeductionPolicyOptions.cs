namespace Api.Application.Services.DeductionPolicies;

public class DependentsAgeSurchargeDeductionPolicyOptions
{
    public const string Key = "DependentsAgeSurchargeDeductionPolicy";

    public int AgeThreshold { get; init; }
    public decimal MonthlySurchargePerDependent { get; init; }
}