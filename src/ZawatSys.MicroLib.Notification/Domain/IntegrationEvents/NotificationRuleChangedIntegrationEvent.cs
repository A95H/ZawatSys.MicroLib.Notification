namespace ZawatSys.MicroLib.Notification.Domain.IntegrationEvents;

/// <summary>
/// Raised when a NotificationRule entity is created, updated,
/// activated, or deactivated. Allows projections to refresh
/// rule-dependent behavior.
/// </summary>
public sealed record NotificationRuleChangedIntegrationEvent
{
    public Guid RuleId { get; init; }
    public Guid TenantId { get; init; }
    public string? RuleKey { get; init; }
    public string? EventType { get; init; }
    public NotificationRuleChangeType ChangeType { get; init; }
    public DateTimeOffset ChangedAt { get; init; }
}

/// <summary>
/// Type of notification rule change.
/// </summary>
public enum NotificationRuleChangeType
{
    Created = 0,
    Updated = 1,
    Activated = 2,
    Deactivated = 3
}
