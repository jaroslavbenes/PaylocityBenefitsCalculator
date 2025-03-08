using System.Collections.Generic;
using System.Linq;
using Api.Models;
using Api.Services;
using Api.Services.Abstractions;
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
}