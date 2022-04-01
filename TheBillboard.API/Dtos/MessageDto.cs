namespace TheBillboard.API.Dtos;

using Domain;
using System.ComponentModel.DataAnnotations;

public record MessageDto
    (
    [Required, StringLength(10)]
    string Title,
    [Required]
    string Body,
    [Required]
    int AuthorId,
    int? Id
    )
{
    public MessageDto() : this(string.Empty, string.Empty, 0, null)
    { }

    public MessageDto(Message message)
        : this(message.Title, message.Body, message.AuthorId, message.Id)
    { }
}