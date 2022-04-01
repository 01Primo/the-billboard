namespace TheBillboard.API.Dtos;

using System.ComponentModel.DataAnnotations;

    public class MessageDtoWithDateAndAuthor : MessageDto
    {
        [Required]
        public DateTime? CreatedAt { get; init; }
        [Required]
        public DateTime? UpdatedAt { get; init; }
        [Required]
        public AuthorDtoWithDate Author { get; init; }
    }

