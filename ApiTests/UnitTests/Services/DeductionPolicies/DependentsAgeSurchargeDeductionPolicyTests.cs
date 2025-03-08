using System;
using System.Collections.Generic;
using Api.Application.Models;
using Api.Application.Services.DeductionPolicies;
using Microsoft.Extensions.Options;
using Xunit;

namespace ApiTests.UnitTests.Services.DeductionPolicies;

public class DependentsAgeSurchargeDeductionPolicyTests
{
    private static DependentsAgeSurchargeDeductionPolicy CreatePolicy(int ageThreshold, decimal monthlySurchargePerDependent)
    {
        var options =
            Options.Create(
                new DependentsAgeSurchargeDeductionPolicyOptions
                {
                    AgeThreshold = ageThreshold,
                    MonthlySurchargePerDependent = monthlySurchargePerDependent
                });

        return new DependentsAgeSurchargeDeductionPolicy(options);
    }

    [Fact]
    public void Name_ShouldBeNonEmptyString()
    {
        // Arrange
        var policy = CreatePolicy(50, 200);

        // Act
        var result = policy.Name;

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(result));
    }

    [Fact]
    public void IsApplicable_ShouldReturnTrue_WhenEmployeeHasSomeDependentOver50()
    {
        // Arrange
        var policy = CreatePolicy(50, 200);
        var employee = new Employee
        {
            Dependents =
            [
                new Dependent { DateOfBirth = DateTime.Today.AddYears(-50) },
                new Dependent { DateOfBirth = DateTime.Today.AddYears(-49) }
            ]
        };

        // Act
        var result = policy.IsApplicable(employee);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsApplicable_ShouldReturnFalse_WhenEmployeeHasNoDependentOver50()
    {
        // Arrange
        var policy = CreatePolicy(50, 200);
        var employee = new Employee
        {
            Dependents =
            [
                new Dependent { DateOfBirth = DateTime.Today.AddYears(-49) },
                new Dependent { DateOfBirth = DateTime.Today.AddYears(-48) }
            ]
        };

        // Act
        var result = policy.IsApplicable(employee);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Calculate_ShouldReturnExpectedAmount_WhenEmployeeHasSomeDependentOver50()
    {
        // Arrange
        var policy = CreatePolicy(50, 200);

        var employee = new Employee
        {
            Dependents = new List<Dependent>
            {
                new Dependent { DateOfBirth = DateTime.Today.AddYears(-50) },
                new Dependent { DateOfBirth = DateTime.Today.AddYears(-49) }
            }
        };

        const int paychecksPerYear = 26;
        const decimal monthlySurchargePerDependent = 200;
        const int dependentsOver50Count = 1;
        const decimal expectedAmount = dependentsOver50Count * monthlySurchargePerDependent * 12 / paychecksPerYear;

        // Act
        var result = policy.Calculate(employee, paychecksPerYear);

        // Assert
        Assert.Equal(expectedAmount, result);
    }

    [Fact]
    public void Calculate_ShouldReturnZero_WhenEmployeeHasNoDependentOver50()
    {
        // Arrange
        var policy = CreatePolicy(50, 200);

        var employee = new Employee
        {
            Dependents = new List<Dependent>
            {
                new Dependent { DateOfBirth = DateTime.Today.AddYears(-49) },
                new Dependent { DateOfBirth = DateTime.Today.AddYears(-48) }
            }
        };

        const int paychecksPerYear = 26;

        // Act
        var result = policy.Calculate(employee, paychecksPerYear);

        // Assert
        Assert.Equal(0, result);
    }
}