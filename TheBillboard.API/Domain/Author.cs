using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheBillboard.API.Domain;

[Table("Author")]
public record Author
(
    [Required]
    string Name,
    [Required]
    string Surname,
    [Required]
    string Mail,
    [Required]
    DateTime CreatedAt,
    [Required]
    DateTime UpdatedAt
)
{
    [Key]
    public int? Id { get; set; }
    public virtual IReadOnlyCollection<Message> Messages { get; set; } = null!;
}