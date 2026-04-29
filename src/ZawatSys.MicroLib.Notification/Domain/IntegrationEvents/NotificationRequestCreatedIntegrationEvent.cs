using ZawatSys.MicroLib.Shared.Contracts.Common;

namespace ZawatSys.MicroLib.Notification.Domain.IntegrationEvents;

/// <summary>
/// Raised when a new NotificationRequest has been created and persisted
/// in the Pending state. The request is now queued for dispatch workers.
/// </summary>
public sealed record NotificationRequestCreatedIntegrationEvent : IDomainIntegrationEvent
{
    public Guid RequestId { get; init; }
    public Guid TenantId { get; init; }
    public string IdempotencyKey { get; init; } = string.Empty;
    public string TemplateKey { get; init; } = string.Empty;
    public IReadOnlyList<RecipientTargetSnapshot> Recipients { get; init; } = [];
    public IReadOnlyList<string> DefaultChannels { get; init; } = [];
    public string? RuleKey { get; init; }
    public string CorrelationId { get; init; } = string.Empty;
    public string? TraceId { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
}

/// <summary>
/// Snapshot of a recipient target captured at request creation time.
/// </summary>
public sealed record RecipientTargetSnapshot
{
    public Guid RecipientId { get; init; }
    public string? DisplayName { get; init; }
    public IReadOnlyDictionary<string, string> Metadata { get; init; } = null!;
}
