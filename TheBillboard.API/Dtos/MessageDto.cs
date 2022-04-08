namespace TheBillboard.API.Dtos;

using System.ComponentModel.DataAnnotations;

public record MessageDto
{
    [Required, StringLength(10)]
    public string Title { get; init; } = string.Empty;
    
    [Required]
    public string Body { get; init; } = string.Empty;
    
    [Required]
    public int AuthorId { get; init; }
    
    public int? Id { get; init; }
}