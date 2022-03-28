namespace TheBillboard.API.Domain;

public record Author
(
    string Name = "",
    string Surname = "",
    int? Id = default,
    string? Email = "",
    DateTime? CreatedAt = null,
    DateTime? UpdatedAt = null
) : BaseEntity(Id, CreatedAt, UpdatedAt)
{
    public Author(int? id, string name, string surname, string? mail, DateTime? createdAt, DateTime? updatedAt) : this(name, surname, id, mail, createdAt, updatedAt)
    {
    }

    public override string ToString()
    {
        return Name + " " + Surname;
    }
}