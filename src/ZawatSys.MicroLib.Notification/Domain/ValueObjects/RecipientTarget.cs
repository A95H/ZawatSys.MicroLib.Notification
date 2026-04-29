using ZawatSys.MicroLib.Notification.Domain.Enums;

namespace ZawatSys.MicroLib.Notification.Domain.ValueObjects;

/// <summary>
/// Immutable snapshot of recipient identity at request creation time.
/// Does not include endpoint data (resolved later from ContactService).
/// </summary>
public sealed class RecipientTarget : IEquatable<RecipientTarget>
{
    public Guid UserId { get; }
    public Guid TenantId { get; }
    public Guid? CorrelationId { get; }
    public string? DisplayName { get; }
    public RecipientType RecipientType { get; }

    private RecipientTarget() { } // EF Core

    public RecipientTarget(
        Guid userId,
        Guid tenantId,
        RecipientType recipientType,
        Guid? correlationId = null,
        string? displayName = null)
    {
        UserId = userId;
        TenantId = tenantId;
        RecipientType = recipientType;
        CorrelationId = correlationId;
        DisplayName = displayName;
    }

    public bool Equals(RecipientTarget? other)
    {
        if (other is null) return false;
        return UserId == other.UserId
            && TenantId == other.TenantId
            && RecipientType == other.RecipientType;
    }

    public override bool Equals(object? obj) => Equals(obj as RecipientTarget);

    public override int GetHashCode() => HashCode.Combine(UserId, TenantId, RecipientType);
}