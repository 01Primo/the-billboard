namespace TheBillboard.API.Domain;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dtos;

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

    public Author(AuthorDto authorDto) : this(authorDto.Name, authorDto.Surname, authorDto.Mail, DateTime.Now, DateTime.Now)
    {
        Id = authorDto.Id;
    }
}