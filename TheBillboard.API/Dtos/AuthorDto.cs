namespace TheBillboard.API.Dtos;

using System.ComponentModel.DataAnnotations;

public class AuthorDto
{
    [Required, StringLength(50)]
    public string Name { get; init; } = string.Empty;

    [Required, StringLength(50)]
    public string Surname { get; init; } = string.Empty;

    public int? Id { get; init; }

    [Required, StringLength(50)]
    public string? Mail { get; init; } = string.Empty;
}