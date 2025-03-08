namespace Api.DtoMappers;

public static class DependentMappers
{
    public static Dtos.Dependent.DependentDto ToDependentDto(this Application.Models.Dependent dependent) =>
        new(
            Id: dependent.Id,
            FirstName: dependent.FirstName,
            LastName: dependent.LastName,
            DateOfBirth: dependent.DateOfBirth,
            Relationship: dependent.Relationship);
}