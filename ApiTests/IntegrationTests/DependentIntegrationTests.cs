using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Api.Application.Models;
using Api.Dtos.Dependent;
using Xunit;

namespace ApiTests.IntegrationTests;

public class DependentIntegrationTests(TestWebApplicationFactory factory) : IClassFixture<TestWebApplicationFactory>
{
    [Fact]
    public async Task WhenAskedForAllDependents_ShouldReturnAllDependents()
    {
        // Arrange
        var client = factory.CreateClient();
        var dependents =
            new List<DependentDto>
            {
                new(1, "Spouse", "Morant", Relationship.Spouse, new DateTime(1998, 3, 3)),
                new(2, "Child1", "Morant", Relationship.Child, new DateTime(2020, 6, 23)),
                new(3, "Child2", "Morant", Relationship.Child, new DateTime(2021, 5, 18)),
                new(4, "DP", "Jordan", Relationship.DomesticPartner, new DateTime(1974, 1, 2))
            };

        // Act
        var response = await client.GetAsync("/api/v1/dependents");

        // Assert
        await response.ShouldReturn(HttpStatusCode.OK, dependents);
    }

    [Fact]
    public async Task WhenAskedForADependent_ShouldReturnCorrectDependent()
    {
        // Arrange
        var client = factory.CreateClient();
        var dependent = new DependentDto(1, "Spouse", "Morant", Relationship.Spouse, new DateTime(1998, 3, 3));

        // Act
        var response = await client.GetAsync("/api/v1/dependents/1");

        // Assert
        await response.ShouldReturn(HttpStatusCode.OK, dependent);
    }

    [Fact]
    public async Task WhenAskedForANonexistentDependent_ShouldReturn404()
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/v1/dependents/{int.MinValue}");

        // Assert
        await response.ShouldReturn(HttpStatusCode.NotFound);
    }
}