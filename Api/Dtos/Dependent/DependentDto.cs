using Api.Application.Models;

namespace Api.Dtos.Dependent;

public sealed record DependentDto(
    int Id,
    string? FirstName,
    string? LastName,
    Relationship Relationship,
    DateTime DateOfBirth);