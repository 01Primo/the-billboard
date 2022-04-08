namespace TheBillboard.API.Domain;

public abstract record EntityBase(
    int? Id = default,
    DateTime? CreatedAt = null,
    DateTime? UpdatedAt = null
    );
