namespace TheBillboard.API.Domain;

public abstract record BaseEntity(
    int? Id = default,
    DateTime? CreatedAt = null,
    DateTime? UpdatedAt = null
    );
