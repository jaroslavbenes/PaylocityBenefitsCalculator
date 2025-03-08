using System;
using System.Collections.Generic;
using Api.Models;
using Api.Services.DeductionPolicies;
using Xunit;

namespace ApiTests.UnitTests.Services.DeductionPolicies;

public class DependentsAgeSurchargeDeductionPolicyTests
{
    [Fact]
    public void Name_ShouldBeNonEmptyString()
    {
        // Arrange
        var policy = new DependentsAgeSurchargeDeductionPolicy();

        // Act
        var result = policy.Name;

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(result));
    }

    [Fact]
    public void IsApplicable_ShouldReturnTrue_WhenEmployeeHasSomeDependentOver50()
    {
        // Arrange
        var policy = new DependentsAgeSurchargeDeductionPolicy();
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
        var policy = new DependentsAgeSurchargeDeductionPolicy();
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
        var policy = new DependentsAgeSurchargeDeductionPolicy();

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
        var policy = new DependentsAgeSurchargeDeductionPolicy();

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