using System.ComponentModel.DataAnnotations;

namespace TheBillboard.API.Dtos;

public record AuthorDto
{
    [Required]
    public string Name { get; init; } = string.Empty;

    [Required]
    public string Surname { get; init; } = string.Empty;

    [Required]
    public string Email { get; init; } = string.Empty;

    public int? Id { get; init; }
}
