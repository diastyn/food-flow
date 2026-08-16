namespace FoodFlow.Modules.Identity.Domain.ValueObjects;

public readonly record struct AuditLogId(Guid Value)
{
    public static AuditLogId New() => new(Guid.NewGuid());

    public static implicit operator Guid(AuditLogId id) => id.Value;

    public static implicit operator AuditLogId(Guid id) => new(id);

    public override string ToString() => Value.ToString();
}
