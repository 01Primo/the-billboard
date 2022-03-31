using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheBillboard.API.Domain;

[Table("Messages")]
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
}