using ZawatSys.MicroLib.Notification.Domain.Enums;

namespace ZawatSys.MicroLib.Notification.Domain.Entities;

/// <summary>
/// Aggregate Root for outbound notification intent and audit.
/// Represents a single notification intent that may generate multiple delivery attempts across channels and recipients.
/// </summary>
public sealed class NotificationRequest
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }
    public string TemplateKey { get; private set; } = string.Empty;
    public string Payload { get; private set; } = "{}"; // JSON
    public RecipientType RecipientType { get; private set; }
    public Guid? CorrelationId { get; private set; }
    public NotificationPriority Priority { get; private set; }
    public NotificationRequestStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? QueuedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private NotificationRequest() { } // EF Core

    public NotificationRequest(
        Guid id,
        Guid tenantId,
        string templateKey,
        RecipientType recipientType,
        NotificationPriority priority,
        Guid? correlationId = null,
        string payload = "{}")
    {
        Id = id;
        TenantId = tenantId;
        TemplateKey = templateKey;
        RecipientType = recipientType;
        Priority = priority;
        CorrelationId = correlationId;
        Payload = payload;
        Status = NotificationRequestStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Transitions the request to Queued state. Cannot transition from Completed or Failed.
    /// </summary>
    public void Queue()
    {
        if (Status == NotificationRequestStatus.Completed || Status == NotificationRequestStatus.Failed)
            throw new InvalidOperationException("Cannot transition from Completed or Failed to Queued.");

        Status = NotificationRequestStatus.Queued;
        QueuedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the request as Completed. Terminal state.
    /// </summary>
    public void Complete()
    {
        if (Status == NotificationRequestStatus.Failed)
            throw new InvalidOperationException("Cannot transition from Failed to Completed.");

        Status = NotificationRequestStatus.Completed;
        CompletedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the request as Failed. Terminal state.
    /// </summary>
    public void Fail()
    {
        if (Status == NotificationRequestStatus.Completed)
            throw new InvalidOperationException("Cannot transition from Completed to Failed.");

        Status = NotificationRequestStatus.Failed;
        CompletedAt = DateTime.UtcNow;
    }
}

/// <summary>
/// Request-level lifecycle states.
/// </summary>
public enum NotificationRequestStatus
{
    Pending = 1,
    Queued = 2,
    Processing = 3,
    Completed = 4,
    Failed = 5
}