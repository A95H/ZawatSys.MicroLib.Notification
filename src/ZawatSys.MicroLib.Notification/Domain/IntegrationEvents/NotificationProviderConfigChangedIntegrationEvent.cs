using ZawatSys.MicroLib.Notification.Domain.Enums;

namespace ZawatSys.MicroLib.Notification.Domain.IntegrationEvents;

/// <summary>
/// Raised when a NotificationProviderConfig entity is created, updated,
/// activated, or deactivated. Allows projections and cache invalidation
/// to stay synchronized with configuration changes.
/// </summary>
public sealed record NotificationProviderConfigChangedIntegrationEvent
{
    public Guid ConfigId { get; init; }
    public Guid TenantId { get; init; }
    public NotificationProviderName ProviderId { get; init; }
    public NotificationChannelCode Channel { get; init; }
    public NotificationProviderConfigChangeType ChangeType { get; init; }
    public DateTimeOffset ChangedAt { get; init; }
}

/// <summary>
/// Type of provider configuration change.
/// </summary>
public enum NotificationProviderConfigChangeType
{
    Created = 0,
    Updated = 1,
    Activated = 2,
    Deactivated = 3
}
