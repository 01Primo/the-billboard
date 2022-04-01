namespace TheBillboard.API.Dtos;

using System.ComponentModel.DataAnnotations;

public class AuthorDtoWithDate : AuthorDto
{
    [Required]
    public DateTime? CreatedAt { get; init; }
    [Required]
    public DateTime? UpdatedAt { get; init; }
}
