using Api.Application.Models;
using Api.Application.Services.DeductionPolicies;
using Microsoft.Extensions.Options;
using Xunit;

namespace ApiTests.UnitTests.Services.DeductionPolicies;

public class DependentsDeductionPolicyTests
{
    private static DependentsDeductionPolicy CreatePolicy(decimal dependentMonthlyCost)
    {
        var options =
            Options.Create(
                new DependentsDeductionPolicyOptions
                {
                    DependentMonthlyCost = dependentMonthlyCost
                });

        return new DependentsDeductionPolicy(options);
    }

    [Fact]
    public void Name_ShouldBeNonEmptyString()
    {
        // Arrange
        var policy = CreatePolicy(600);

        // Act
        var result = policy.Name;

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(result));
    }

    [Fact]
    public void IsApplicable_ShouldReturnTrue_WhenEmployeeHasSomeDependents()
    {
        // Arrange
        var policy = CreatePolicy(600);
        var employee = new Employee { Dependents = [new Dependent()] };

        // Act
        var result = policy.IsApplicable(employee);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsApplicable_ShouldReturnFalse_WhenEmployeeHasNoDependents()
    {
        // Arrange
        var policy = CreatePolicy(600);
        var employee = new Employee();

        // Act
        var result = policy.IsApplicable(employee);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Calculate_ShouldReturnExpectedAmount_WhenEmployeeHasSomeDependents()
    {
        // Arrange
        var policy = CreatePolicy(600);
        var employee = new Employee { Dependents = [new Dependent(), new Dependent()] };
        const decimal dependentMonthlyCost = 600;
        const int paychecksPerYear = 26;
        var expectedAmount = employee.Dependents.Count * dependentMonthlyCost * 12 / paychecksPerYear;

        // Act
        var result = policy.Calculate(employee, paychecksPerYear);

        // Assert
        Assert.Equal(expectedAmount, result);
    }

    [Fact]
    public void Calculate_ShouldReturnZero_WhenEmployeeHasNoDependents()
    {
        // Arrange
        var policy = CreatePolicy(600);
        var employee = new Employee();

        // Act
        var result = policy.Calculate(employee, 26);

        // Assert
        Assert.Equal(0, result);
    }
}