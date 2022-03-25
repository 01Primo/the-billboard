namespace TheBillboard.API.Domain;

public abstract record BaseObject
(
    int? Id = default,
    DateTime? CreatedAt = null,
    DateTime? UpdatedAt = null
);