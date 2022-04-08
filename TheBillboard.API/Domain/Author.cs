namespace TheBillboard.API.Domain;

public record Author
(
    string Name = "",
    string Surname = "",
    int? Id = default,
    string? Email = "",
    DateTime? CreatedAt = null,
    DateTime? UpdatedAt = null
) : EntityBase(Id, CreatedAt, UpdatedAt)
{
    [System.Text.Json.Serialization.JsonIgnore]
    public ICollection<Message> Messages { get; set; } = null!;
}