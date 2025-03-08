namespace Api.Application.Services.DeductionPolicies;

public class HighEarnerDeductionPolicyOptions
{
    public const string Key = "HighEarnerDeductionPolicy";

    public decimal SalaryThreshold { get; init; }
    public decimal SurchargeRate { get; init; }
}