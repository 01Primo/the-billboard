namespace TheBillboard.API.Domain;

public record Author
(
    string Name = "",
    string Surname = "",
    int? Id = default,
    string? Email = "",
    DateTime? CreatedAt = null,
    DateTime? UpdatedAt = null
)
{
    public Author(int id, string name, string surname, string? mail, DateTime? createdAt) : this(name, surname, id, mail, createdAt)
    {
    }

    public override string ToString()
    {
        return Name + " " + Surname;
    }
}