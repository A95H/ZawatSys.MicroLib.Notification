using ZawatSys.MicroLib.Notification.Domain.Enums;

namespace ZawatSys.MicroLib.Notification.Domain.ValueObjects;

/// <summary>
/// Composite identity for idempotency of a delivery attempt.
/// Uniqueness: (RequestId, RecipientId, Channel, ProviderId).
/// </summary>
public sealed class AttemptIdentity : IEquatable<AttemptIdentity>
{
    public Guid NotificationRequestId { get; }
    public Guid RecipientId { get; }
    public NotificationChannelCode Channel { get; }
    public NotificationProviderName ProviderId { get; }

    private AttemptIdentity() { } // EF Core

    public AttemptIdentity(
        Guid notificationRequestId,
        Guid recipientId,
        NotificationChannelCode channel,
        NotificationProviderName providerId)
    {
        NotificationRequestId = notificationRequestId;
        RecipientId = recipientId;
        Channel = channel;
        ProviderId = providerId;
    }

    public bool Equals(AttemptIdentity? other)
    {
        if (other is null) return false;
        return NotificationRequestId == other.NotificationRequestId
            && RecipientId == other.RecipientId
            && Channel == other.Channel
            && ProviderId == other.ProviderId;
    }

    public override bool Equals(object? obj) => Equals(obj as AttemptIdentity);

    public override int GetHashCode() => HashCode.Combine(NotificationRequestId, RecipientId, Channel, ProviderId);
}