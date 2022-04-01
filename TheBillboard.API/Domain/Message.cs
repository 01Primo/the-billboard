namespace TheBillboard.API.Domain;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dtos;

[Table("Message")]
public record Message
(
    [Required]
    string Title,
    [Required]
    string Body,
    [Required]
    DateTime CreatedAt,
    [Required]
    DateTime UpdatedAt
)
{
    [Key]
    public int? Id { get; set; }
    public virtual Author Author { get; set; } = null!;
    [ForeignKey("Author")]
    public int AuthorId { get; set; }

    public Message(MessageDto msgDto) : this(msgDto.Title, msgDto.Body, DateTime.Now, DateTime.Now)
    {
        AuthorId = msgDto.AuthorId;
        Id = msgDto.Id;
    }
}