using ZawatSys.MicroLib.Notification.Domain.Enums;

namespace ZawatSys.MicroLib.Notification.Domain.IntegrationEvents;

/// <summary>
/// Raised when a new NotificationDeliveryAttempt record has been created
/// and persisted in the Pending state, ready for dispatch worker processing.
/// </summary>
public sealed record NotificationDeliveryAttemptCreatedIntegrationEvent
{
    public Guid AttemptId { get; init; }
    public Guid RequestId { get; init; }
    public Guid TenantId { get; init; }
    public Guid RecipientId { get; init; }
    public NotificationChannelCode Channel { get; init; }
    public NotificationProviderName ProviderId { get; init; }
    public Guid ProviderConfigId { get; init; }
    public string TemplateKey { get; init; } = string.Empty;
    public string? ProviderTemplateKey { get; init; }
    public EndpointSnapshot Endpoint { get; init; } = new();
    public int AttemptNumber { get; init; }
    public string CorrelationId { get; init; } = string.Empty;
    public DateTimeOffset CreatedAt { get; init; }
}

/// <summary>
/// Snapshot of the resolved endpoint at the time of attempt creation.
/// </summary>
public sealed record EndpointSnapshot
{
    public string Value { get; init; } = string.Empty;
    public NotificationChannelCode Channel { get; init; }
    public bool IsVerified { get; init; }
}
