using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.Application.Models;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Dtos.PayStub;
using Xunit;

namespace ApiTests.IntegrationTests;

public class EmployeeIntegrationTests(TestWebApplicationFactory factory) : IClassFixture<TestWebApplicationFactory>
{
    [Fact]
    public async Task WhenAskedForAllEmployees_ShouldReturnAllEmployees()
    {
        // Arrange
        var client = factory.CreateClient();
        var employees =
            new List<EmployeeDto>
            {
                new(1, "LeBron", "James", 75420.99m, new DateTime(1984, 12, 30), []),
                new(
                    2,
                    "Ja",
                    "Morant",
                    92365.22m,
                    new DateTime(1999, 8, 10),
                    new List<DependentDto>
                    {
                        new(1, "Spouse", "Morant", Relationship.Spouse, new DateTime(1998, 3, 3)),
                        new(2, "Child1", "Morant", Relationship.Child, new DateTime(2020, 6, 23)),
                        new(3, "Child2", "Morant", Relationship.Child, new DateTime(2021, 5, 18)),
                    }),
                new(
                    3,
                    "Michael",
                    "Jordan",
                    143211.12m,
                    new DateTime(1963, 2, 17),
                    new List<DependentDto>
                    {
                        new(4, "DP", "Jordan", Relationship.DomesticPartner, new DateTime(1974, 1, 2))
                    }
                )
            };

        // Act
        var response = await client.GetAsync("/api/v1/employees");

        // Assert
        await response.ShouldReturn(HttpStatusCode.OK, employees);
    }

    [Fact]
    public async Task WhenAskedForAnEmployee_ShouldReturnCorrectEmployee()
    {
        // Arrange
        var client = factory.CreateClient();
        var employee =
            new EmployeeDto(
                1,
                "LeBron",
                "James",
                75420.99m,
                new DateTime(1984, 12, 30), []);

        // Act
        var response = await client.GetAsync("/api/v1/employees/1");

        // Assert
        await response.ShouldReturn(HttpStatusCode.OK, employee);
    }

    [Fact]
    public async Task WhenAskedForANonexistentEmployee_ShouldReturn404()
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/v1/employees/{int.MinValue}");

        // Assert
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task WhenAskedForEmployeePayStub_ShouldReturnCorrectPayStub()
    {
        // Arrange
        var client = factory.CreateClient();
        var expectedPayStub =
            new PayStubDto(
                3,
                5508.12m,
                new List<DeductionDto>
                {
                    new("Base Benefits Costs", 461.54m),
                    new("Dependents Benefits Costs", 276.92m),
                    new("High Earner 2% Surcharge", 110.16m),
                    new("Dependents age surcharge", 92.31m)
                },
                940.93m,
                4567.19m);

        // Act
        var response = await client.GetAsync("/api/v1/employees/3/paystub");

        // Assert
        await response.ShouldReturn(HttpStatusCode.OK, expectedPayStub);
    }
}