namespace TheBillboard.API.Dtos;

using Domain;
using System.ComponentModel.DataAnnotations;

public record AuthorDto
    (
    [Required, StringLength(10)]
    string Name,
    [Required]
    string Surname,
    [Required]
    string Mail,
    int? Id
    )
{
    public AuthorDto() : this(string.Empty, string.Empty, string.Empty, null)
    { }

    public AuthorDto(Author author)
        : this(author.Name, author.Surname, author.Mail, author.Id)
    { }
}