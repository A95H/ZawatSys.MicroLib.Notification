using ZawatSys.MicroLib.Notification.Domain.Enums;

namespace ZawatSys.MicroLib.Notification.Domain.IntegrationEvents;

/// <summary>
/// Raised when a failed attempt has been rescheduled for retry,
/// indicating the next retry time and current attempt count.
/// </summary>
public sealed record NotificationDeliveryAttemptRetriedIntegrationEvent
{
    public Guid AttemptId { get; init; }
    public Guid RequestId { get; init; }
    public Guid TenantId { get; init; }
    public Guid RecipientId { get; init; }
    public NotificationChannelCode Channel { get; init; }
    public NotificationProviderName ProviderId { get; init; }
    public int AttemptNumber { get; init; }
    public DateTimeOffset ScheduledRetryAt { get; init; }
    public int RetriesRemaining { get; init; }
    public string CorrelationId { get; init; } = string.Empty;
}
