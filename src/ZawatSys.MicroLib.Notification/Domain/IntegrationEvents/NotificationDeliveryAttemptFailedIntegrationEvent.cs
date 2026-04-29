using ZawatSys.MicroLib.Notification.Domain.Enums;

namespace ZawatSys.MicroLib.Notification.Domain.IntegrationEvents;

/// <summary>
/// Raised when a delivery attempt has failed with a classification indicating
/// whether the failure is retryable or terminal. Drives retry scheduling
/// and fallback decisions.
/// </summary>
public sealed record NotificationDeliveryAttemptFailedIntegrationEvent
{
    public Guid AttemptId { get; init; }
    public Guid RequestId { get; init; }
    public Guid TenantId { get; init; }
    public Guid RecipientId { get; init; }
    public NotificationChannelCode Channel { get; init; }
    public NotificationProviderName ProviderId { get; init; }
    public int AttemptNumber { get; init; }
    public string FailureReason { get; init; } = string.Empty;
    public bool IsRetryable { get; init; }
    public int RetryCount { get; init; }
    public string CorrelationId { get; init; } = string.Empty;
    public DateTimeOffset FailedAt { get; init; }
}
