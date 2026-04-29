namespace ZawatSys.MicroLib.Notification.Domain.IntegrationEvents;

/// <summary>
/// Raised when a NotificationRequest has exhausted all retryable attempts
/// with no successful delivery and no further retries possible.
/// Terminal failure state - no additional automatic action will be taken.
/// </summary>
public sealed record NotificationRequestFailedIntegrationEvent
{
    public Guid RequestId { get; init; }
    public Guid TenantId { get; init; }
    public string CorrelationId { get; init; } = string.Empty;
    public DateTimeOffset FailedAt { get; init; }
    public string Reason { get; init; } = string.Empty;
    public IReadOnlyList<FailedAttemptSummary> FailedAttempts { get; init; } = [];
}

/// <summary>
/// Summary of a single failed attempt included in the failure event.
/// </summary>
public sealed record FailedAttemptSummary
{
    public Guid RecipientId { get; init; }
    public string Channel { get; init; } = string.Empty;
    public string ProviderId { get; init; } = string.Empty;
    public int AttemptNumber { get; init; }
    public string FailureReason { get; init; } = string.Empty;
    public bool IsRetryable { get; init; }
}
