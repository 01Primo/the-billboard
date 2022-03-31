namespace TheBillboard.API.Domain;

public record Author
(
    int? Id = default,
    string Name = "",
    string Surname = "",
    string? Mail = "",
    DateTime? CreatedAt = null,
    DateTime? UpdatedAt = null
) : BaseObject(Id, CreatedAt, UpdatedAt)
{
    public IReadOnlyCollection<Message> Messages { get; set; } = new HashSet<Message>();
    public override string ToString() => Name + " " + Surname;
}