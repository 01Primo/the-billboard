namespace TheBillboard.API.Domain;

using System.ComponentModel.DataAnnotations;

public record Message : BaseObject
{
    public string Title { get; init; }
    public string Body { get; init; }
    public int AuthorId { get; init; }  
    public Author? Author { get; }

    public Message(
        int? id = default,
        string title = "",
        [Required(ErrorMessage = "Il campo Message e' obbligatorio")] [MinLength(5, ErrorMessage = "Il campo Message deve essere lungo almento 5 caratteri")]
        string body = "",
        int authorId = default,       
        DateTime? createdAt = default,
        DateTime? updatedAt = default
        ) : base(id, createdAt, updatedAt)
    {
        Title = title;
        Body = body;
        AuthorId = authorId;        
    }

    public Message(int id, string title, string body, int authorId, string name, string surname, string email, 
                   DateTime messageCreatedAt, DateTime messageUpdatedAt, DateTime authorCreatedAt, DateTime authorUpdatedAt)
                   : base(id, messageCreatedAt, messageUpdatedAt)
    {
        Title = title;
        Body = body;
        AuthorId = authorId;        
    }

    public string FormattedCreatedAt => CreatedAt.HasValue
        ? CreatedAt.Value.ToString("yyyy-MM-dd HH:mm")
        : string.Empty;

    public string FormattedUpdatedAt => UpdatedAt.HasValue
        ? UpdatedAt.Value.ToString("yyyy-MM-dd HH:mm")
        : string.Empty;
}