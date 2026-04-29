using ZawatSys.MicroLib.Notification.Domain.Enums;
using ZawatSys.MicroLib.Notification.Domain.ValueObjects;

namespace ZawatSys.MicroLib.Notification.Domain.Entities;

/// <summary>
/// Per-recipient, per-channel, per-provider delivery record.
/// Uniquely identified by (NotificationRequestId, RecipientId, Channel, ProviderId).
/// </summary>
public sealed class NotificationDeliveryAttempt
{
    public Guid Id { get; private set; }
    public Guid NotificationRequestId { get; private set; }
    public Guid RecipientId { get; private set; }
    public NotificationChannelCode Channel { get; private set; }
    public NotificationProviderName ProviderName { get; private set; }
    public NotificationAttemptStatus Status { get; private set; }
    public int AttemptNumber { get; private set; }
    public DateTime ScheduledAt { get; private set; }
    public DateTime? SentAt { get; private set; }
    public DateTime? AcknowledgedAt { get; private set; }
    public string? FailureReason { get; private set; }
    public bool Retryable { get; private set; }

    private NotificationDeliveryAttempt() { } // EF Core

    public NotificationDeliveryAttempt(
        Guid id,
        Guid notificationRequestId,
        Guid recipientId,
        NotificationChannelCode channel,
        NotificationProviderName providerId,
        int attemptNumber = 1)
    {
        Id = id;
        NotificationRequestId = notificationRequestId;
        RecipientId = recipientId;
        Channel = channel;
        ProviderName = providerId;
        AttemptNumber = attemptNumber;
        Status = NotificationAttemptStatus.Pending;
        ScheduledAt = DateTime.UtcNow;
        Retryable = false;
    }

    /// <summary>
    /// Transitions to Dispatching state (provider called).
    /// </summary>
    public void Dispatch()
    {
        Status = NotificationAttemptStatus.Dispatching;
    }

    /// <summary>
    /// Transitions to Acknowledged state (terminal success for non-send-only channels).
    /// For send-only channels (WhatsApp, Telegram), Sent == Acknowledged.
    /// </summary>
    public void Acknowledge()
    {
        Status = NotificationAttemptStatus.Acknowledged;
        AcknowledgedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Records a failed attempt with optional retryable classification.
    /// </summary>
    public void Fail(string? failureReason, bool retryable = false)
    {
        Status = retryable ? NotificationAttemptStatus.Retryable : NotificationAttemptStatus.Failed;
        FailureReason = failureReason;
        Retryable = retryable;
        SentAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Reschedules a retryable attempt.
    /// </summary>
    public void Retry()
    {
        if (Status != NotificationAttemptStatus.Retryable)
            throw new InvalidOperationException("Only Retryable attempts can be retried.");

        Status = NotificationAttemptStatus.Pending;
        AttemptNumber++;
        ScheduledAt = DateTime.UtcNow;
        FailureReason = null;
        Retryable = false;
    }
}

/// <summary>
/// Attempt-level lifecycle states.
/// </summary>
public enum NotificationAttemptStatus
{
    Pending = 1,
    Dispatching = 2,
    Sent = 3,
    Acknowledged = 4,
    Failed = 5,
    Retryable = 6
}