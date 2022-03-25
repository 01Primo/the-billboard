namespace TheBillboard.API.Dtos;

using System.ComponentModel.DataAnnotations;

public class AuthorDto
{
    [Required, StringLength(10)]
    public string Name { get; init; } = string.Empty;

    [Required, StringLength(10)]
    public string Surname { get; init; } = string.Empty;

    public int? Id { get; init; }

    [Required, StringLength(10)]
    public string? Email { get; init; } = string.Empty;

    [Required]
    public DateTime? CreatedAt { get; init; } = null;

    [Required]
    public DateTime? UpdatedAt { get; init; } = null;
}