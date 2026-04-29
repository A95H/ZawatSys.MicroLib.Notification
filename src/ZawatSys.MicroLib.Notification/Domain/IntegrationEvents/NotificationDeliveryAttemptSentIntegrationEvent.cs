using ZawatSys.MicroLib.Notification.Domain.Enums;

namespace ZawatSys.MicroLib.Notification.Domain.IntegrationEvents;

/// <summary>
/// Raised when a delivery attempt has been sent to the provider
/// and the provider has accepted the delivery request (2xx for Email/SMS,
/// queued status for Twilio, etc.).
/// </summary>
public sealed record NotificationDeliveryAttemptSentIntegrationEvent
{
    public Guid AttemptId { get; init; }
    public Guid RequestId { get; init; }
    public Guid TenantId { get; init; }
    public Guid RecipientId { get; init; }
    public NotificationChannelCode Channel { get; init; }
    public NotificationProviderName ProviderId { get; init; }
    public int AttemptNumber { get; init; }
    public string? ProviderMessageId { get; init; }
    public string CorrelationId { get; init; } = string.Empty;
    public DateTimeOffset SentAt { get; init; }
}
