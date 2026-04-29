namespace ZawatSys.MicroLib.Notification.Domain.IntegrationEvents;

/// <summary>
/// Raised when all required delivery attempts for a NotificationRequest
/// have reached Acknowledged (success) or terminal Failed state.
/// The request lifecycle is complete.
/// </summary>
public sealed record NotificationRequestCompletedIntegrationEvent
{
    public Guid RequestId { get; init; }
    public Guid TenantId { get; init; }
    public string CorrelationId { get; init; } = string.Empty;
    public DateTimeOffset CompletedAt { get; init; }
    public int TotalAttempts { get; init; }
    public int SuccessfulAttempts { get; init; }
    public int FailedAttempts { get; init; }
}
