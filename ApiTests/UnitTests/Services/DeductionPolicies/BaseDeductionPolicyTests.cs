using Api.Application.Models;
using Api.Application.Services.DeductionPolicies;
using Microsoft.Extensions.Options;
using Xunit;

namespace ApiTests.UnitTests.Services.DeductionPolicies;

public class BaseDeductionPolicyTests
{
    private static BaseDeductionPolicy CreatePolicy(decimal baseMonthlyCost)
    {
        var options =
            Options.Create(
                new BaseDeductionPolicyOptions
                {
                    BaseMonthlyCost = baseMonthlyCost
                });

        return new BaseDeductionPolicy(options);
    }

    [Fact]
    public void Name_ShouldBeNonEmptyString()
    {
        // Arrange
        var policy = CreatePolicy(1_000);

        // Act
        var result = policy.Name;

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(result));
    }

    [Fact]
    public void IsApplicable_ShouldReturnTrue()
    {
        // Arrange
        var policy = CreatePolicy(1_000);
        var employee = new Employee();

        // Act
        var result = policy.IsApplicable(employee);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Calculate_ShouldReturnExpectedAmount()
    {
        // Arrange
        var policy = CreatePolicy(1_000);
        var employee = new Employee();
        const decimal baseMonthlyCost = 1_000;
        const int paychecksPerYear = 26;
        const decimal expectedAmount = baseMonthlyCost * 12 / paychecksPerYear;

        // Act
        var result = policy.Calculate(employee, paychecksPerYear);

        // Assert
        Assert.Equal(expectedAmount, result);
    }
}