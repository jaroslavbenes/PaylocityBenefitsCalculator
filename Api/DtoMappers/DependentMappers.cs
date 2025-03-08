using Api.Dtos.Dependent;
using Api.Models;

namespace Api.DtoMappers;

public static class DependentMappers
{
    public static GetDependentDto ToGetDependentDto(this Dependent dependent) =>
        new()
        {
            Id = dependent.Id,
            FirstName = dependent.FirstName,
            LastName = dependent.LastName,
            DateOfBirth = dependent.DateOfBirth,
            Relationship = dependent.Relationship
        };
}