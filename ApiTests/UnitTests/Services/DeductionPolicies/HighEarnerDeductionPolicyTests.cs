using Api.Application.Models;
using Api.Application.Services.DeductionPolicies;
using Microsoft.Extensions.Options;
using Xunit;

namespace ApiTests.UnitTests.Services.DeductionPolicies;

public class HighEarnerDeductionPolicyTests
{
    private static HighEarnerDeductionPolicy CreatePolicy(decimal salaryThreshold, decimal surchargeRate)
    {
        var options =
            Options.Create(
                new HighEarnerDeductionPolicyOptions
                {
                    SalaryThreshold = salaryThreshold,
                    SurchargeRate = surchargeRate
                });

        return new HighEarnerDeductionPolicy(options);
    }

    [Fact]
    public void Name_ShouldBeNonEmptyString()
    {
        // Arrange
        var policy = CreatePolicy(80_000, 0.02m);

        // Act
        var result = policy.Name;

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(result));
    }

    [Fact]
    public void IsApplicable_ShouldReturnTrue_WhenEmployeeIsHighEarner()
    {
        // Arrange
        var policy = CreatePolicy(80_000, 0.02m);
        var employee = new Employee { Salary = 80_001 };

        // Act
        var result = policy.IsApplicable(employee);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsApplicable_ShouldReturnFalse_WhenEmployeeIsNotHighEarner()
    {
        // Arrange
        var policy = CreatePolicy(80_000, 0.02m);
        var employee = new Employee { Salary = 80_000 };

        // Act
        var result = policy.IsApplicable(employee);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Calculate_ShouldReturnExpectedAmount_WhenEmployeeIsHighEarner()
    {
        // Arrange
        var policy = CreatePolicy(80_000, 0.02m);
        var employee = new Employee { Salary = 80_001 };
        const int paychecksPerYear = 26;
        var expectedAmount = employee.Salary * 0.02m / paychecksPerYear;

        // Act
        var result = policy.Calculate(employee, paychecksPerYear);

        // Assert
        Assert.Equal(expectedAmount, result);
    }

    [Fact]
    public void Calculate_ShouldReturnZero_WhenEmployeeIsNotHighEarner()
    {
        // Arrange
        var policy = CreatePolicy(80_000, 0.02m);
        var employee = new Employee { Salary = 80_000 };
        const int paychecksPerYear = 26;

        // Act
        var result = policy.Calculate(employee, paychecksPerYear);

        // Assert
        Assert.Equal(0, result);
    }
}