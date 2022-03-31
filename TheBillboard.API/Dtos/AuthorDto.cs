namespace TheBillboard.API.Dtos;
using System.ComponentModel.DataAnnotations;

public class AuthoDto
{

    [Required, StringLength(10)]

    public string Name { get; set; } = string.Empty;

    [Required]

    public string Surname { get; set; } = string.Empty;

    [Required]

    public string Email { get; set; } = string.Empty;

}