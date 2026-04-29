using ZawatSys.MicroLib.Notification.Domain.Enums;

namespace ZawatSys.MicroLib.Notification.Domain.IntegrationEvents;

/// <summary>
/// Raised when a delivery attempt has been acknowledged by the provider
/// as successfully delivered. Terminal success state for the attempt.
/// </summary>
public sealed record NotificationDeliveryAttemptAcknowledgedIntegrationEvent
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
    public DateTimeOffset AcknowledgedAt { get; init; }
}
