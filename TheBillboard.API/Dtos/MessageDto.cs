namespace TheBillboard.API.Dtos;

using System.ComponentModel.DataAnnotations;

public class MessageDto
{
    [Required, StringLength(50)]
    public string Title { get; init; } = string.Empty;
    
    [Required, StringLength(256)]
    public string Body { get; init; } = string.Empty;
    
    [Required]
    public int AuthorId { get; init; }
    
    public int? Id { get; init; }
}