namespace TheBillboard.API.Domain;

using System.ComponentModel.DataAnnotations;

public record Message(
    int? Id = default,
    string Title = "",
    string Body = "",
    int AuthorId = default,
    DateTime? CreatedAt = null,
    DateTime? UpdatedAt = null
) : BaseObject(Id, CreatedAt, UpdatedAt)
{
    public Author? Author { get; }

    public string FormattedCreatedAt => CreatedAt.HasValue
        ? CreatedAt.Value.ToString("yyyy-MM-dd HH:mm")
        : string.Empty;

    public string FormattedUpdatedAt => UpdatedAt.HasValue
        ? UpdatedAt.Value.ToString("yyyy-MM-dd HH:mm")
        : string.Empty;
}