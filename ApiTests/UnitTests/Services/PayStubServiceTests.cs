using System.Collections.Generic;
using System.Linq;
using Api.Application.Models;
using Api.Application.Services;
using Api.Application.Services.Abstractions;
using NSubstitute;
using Xunit;

namespace ApiTests.UnitTests.Services;

public class PayStubServiceTests
{
    [Fact]
    public void GetPayStub_ShouldReturnCorrectPayStub()
    {
        // Arrange
        const string deductionName = "Health Insurance";
        const decimal deductionAmount = 500;
        var employee = new Employee { Id = 1, Salary = 52000 };
        var deductionPolicy = Substitute.For<IDeductionPolicy>();
        deductionPolicy.IsApplicable(employee).Returns(true);
        deductionPolicy.Name.Returns(deductionName);
        deductionPolicy.Calculate(employee, 26).Returns(deductionAmount);
        var deductionPolicies = new List<IDeductionPolicy> { deductionPolicy };
        var service = new PayStubService(deductionPolicies);
        var expectedNetPay = employee.Salary / 26 - deductionAmount;

        // Act
        var payStub = service.GetPayStub(employee);

        // Assert
        Assert.Equal(2000, payStub.GrossPay);
        Assert.Single(payStub.Deductions);
        Assert.Equal(deductionName, payStub.Deductions.First().Name);
        Assert.Equal(deductionAmount, payStub.Deductions.First().Amount);
        Assert.Equal(deductionAmount, payStub.DeductionsTotal);
        Assert.Equal(expectedNetPay, payStub.NetPay);
    }

    [Fact]
    public void GetPayStub_ShouldReturnZeroDeductions_WhenNoPoliciesAreApplicable()
    {
        // Arrange
        var employee = new Employee { Id = 1, Salary = 52000 };
        var deductionPolicy = Substitute.For<IDeductionPolicy>();
        deductionPolicy.IsApplicable(employee).Returns(false);
        var deductionPolicies = new List<IDeductionPolicy> { deductionPolicy };
        var service = new PayStubService(deductionPolicies);
        var expectedNetPay = employee.Salary / 26;

        // Act
        var payStub = service.GetPayStub(employee);

        // Assert
        Assert.Equal(2000, payStub.GrossPay);
        Assert.Empty(payStub.Deductions);
        Assert.Equal(0, payStub.DeductionsTotal);
        Assert.Equal(expectedNetPay, payStub.NetPay);
    }

    [Fact]
    public void GetPayStub_ShouldHandleMultipleDeductions()
    {
        // Arrange
        var employee = new Employee { Id = 1, Salary = 52000 };
        var deductionPolicy1 = Substitute.For<IDeductionPolicy>();
        deductionPolicy1.IsApplicable(employee).Returns(true);
        deductionPolicy1.Name.Returns("Health Insurance");
        deductionPolicy1.Calculate(employee, 26).Returns(500);
        var deductionPolicy2 = Substitute.For<IDeductionPolicy>();
        deductionPolicy2.IsApplicable(employee).Returns(true);
        deductionPolicy2.Name.Returns("Retirement Fund");
        deductionPolicy2.Calculate(employee, 26).Returns(300);
        var deductionPolicies = new List<IDeductionPolicy> { deductionPolicy1, deductionPolicy2 };
        var service = new PayStubService(deductionPolicies);
        var expectedNetPay = employee.Salary / 26 - 500 - 300;

        // Act
        var payStub = service.GetPayStub(employee);

        // Assert
        Assert.Equal(2000, payStub.GrossPay);
        Assert.Equal(2, payStub.Deductions.Count());
        Assert.Equal(800, payStub.DeductionsTotal);
        Assert.Equal(expectedNetPay, payStub.NetPay);
    }
}