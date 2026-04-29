using ZawatSys.MicroLib.Shared.Common.Enums.Core;
using ZawatSys.MicroLib.Shared.Contracts.Common;

namespace ZawatSys.MicroLib.Notification.Domain.Events;

/// <summary>
/// Domain events for Notification domain.
/// These events are raised by domain entities and dispatched to the outbox
/// for reliable asynchronous delivery.
/// </summary>
public interface INotificationDomainEvent : IDomainIntegrationEvent
{
    Guid RequestId { get; }
    Guid TenantId { get; }
}

/// <summary>
/// Raised when a new notification request is created.
/// </summary>
public sealed class NotificationRequestCreatedEvent : INotificationDomainEvent
{
    public Guid EventId { get; }
    public Guid? CorrelationId { get; }
    public Guid IdentityContextId { get; }
    public bool IsSuccess { get; }
    public IssuedBy IssuedBy { get; }
    public string Source { get; }
    public Guid TenantId { get; }
    public IReadOnlyList<string> ErrorMessages { get; }
    public DateTimeOffset Timestamp { get; }
    public Guid RequestId { get; }
    public string TemplateKey { get; }
    public Guid? Correlation { get; }

    public NotificationRequestCreatedEvent(
        Guid requestId,
        Guid tenantId,
        string templateKey,
        Guid? correlation = null)
    {
        EventId = Guid.NewGuid();
        CorrelationId = correlation;
        IdentityContextId = Guid.NewGuid();
        IsSuccess = true;
        IssuedBy = IssuedBy.User;
        Source = "NotificationService";
        TenantId = tenantId;
        ErrorMessages = Array.Empty<string>();
        Timestamp = DateTimeOffset.UtcNow;
        RequestId = requestId;
        TemplateKey = templateKey;
        Correlation = correlation;
    }
}

/// <summary>
/// Raised when a notification request transitions to Queued state.
/// </summary>
public sealed class NotificationRequestQueuedEvent : INotificationDomainEvent
{
    public Guid EventId { get; }
    public Guid? CorrelationId { get; }
    public Guid IdentityContextId { get; }
    public bool IsSuccess { get; }
    public IssuedBy IssuedBy { get; }
    public string Source { get; }
    public Guid TenantId { get; }
    public IReadOnlyList<string> ErrorMessages { get; }
    public DateTimeOffset Timestamp { get; }
    public Guid RequestId { get; }

    public NotificationRequestQueuedEvent(Guid requestId, Guid tenantId, Guid? correlationId = null)
    {
        EventId = Guid.NewGuid();
        CorrelationId = correlationId;
        IdentityContextId = Guid.NewGuid();
        IsSuccess = true;
        IssuedBy = IssuedBy.User;
        Source = "NotificationService";
        TenantId = tenantId;
        ErrorMessages = Array.Empty<string>();
        Timestamp = DateTimeOffset.UtcNow;
        RequestId = requestId;
    }
}

/// <summary>
/// Raised when a delivery attempt is created.
/// </summary>
public sealed class NotificationAttemptCreatedEvent : INotificationDomainEvent
{
    public Guid EventId { get; }
    public Guid? CorrelationId { get; }
    public Guid IdentityContextId { get; }
    public bool IsSuccess { get; }
    public IssuedBy IssuedBy { get; }
    public string Source { get; }
    public Guid TenantId { get; }
    public IReadOnlyList<string> ErrorMessages { get; }
    public DateTimeOffset Timestamp { get; }
    public Guid RequestId { get; }
    public Guid AttemptId { get; }
    public Guid RecipientId { get; }
    public string Channel { get; }
    public string ProviderId { get; }
    public int AttemptNumber { get; }

    public NotificationAttemptCreatedEvent(
        Guid requestId,
        Guid tenantId,
        Guid attemptId,
        Guid recipientId,
        string channel,
        string providerId,
        int attemptNumber,
        Guid? correlationId = null)
    {
        EventId = Guid.NewGuid();
        CorrelationId = correlationId;
        IdentityContextId = Guid.NewGuid();
        IsSuccess = true;
        IssuedBy = IssuedBy.User;
        Source = "NotificationService";
        TenantId = tenantId;
        ErrorMessages = Array.Empty<string>();
        Timestamp = DateTimeOffset.UtcNow;
        RequestId = requestId;
        AttemptId = attemptId;
        RecipientId = recipientId;
        Channel = channel;
        ProviderId = providerId;
        AttemptNumber = attemptNumber;
    }
}

/// <summary>
/// Raised when a delivery attempt reaches a terminal state (Acknowledged or Failed).
/// </summary>
public sealed class NotificationAttemptCompletedEvent : INotificationDomainEvent
{
    public Guid EventId { get; }
    public Guid? CorrelationId { get; }
    public Guid IdentityContextId { get; }
    public bool IsSuccess { get; }
    public IssuedBy IssuedBy { get; }
    public string Source { get; }
    public Guid TenantId { get; }
    public IReadOnlyList<string> ErrorMessages { get; }
    public DateTimeOffset Timestamp { get; }
    public Guid RequestId { get; }
    public Guid AttemptId { get; }
    public Guid RecipientId { get; }
    public string Channel { get; }
    public string ProviderId { get; }
    public string Status { get; }
    public bool IsTerminalSuccess { get; }

    public NotificationAttemptCompletedEvent(
        Guid requestId,
        Guid tenantId,
        Guid attemptId,
        Guid recipientId,
        string channel,
        string providerId,
        string status,
        bool isTerminalSuccess,
        Guid? correlationId = null)
    {
        EventId = Guid.NewGuid();
        CorrelationId = correlationId;
        IdentityContextId = Guid.NewGuid();
        IsSuccess = isTerminalSuccess;
        IssuedBy = IssuedBy.User;
        Source = "NotificationService";
        TenantId = tenantId;
        ErrorMessages = Array.Empty<string>();
        Timestamp = DateTimeOffset.UtcNow;
        RequestId = requestId;
        AttemptId = attemptId;
        RecipientId = recipientId;
        Channel = channel;
        ProviderId = providerId;
        Status = status;
        IsTerminalSuccess = isTerminalSuccess;
    }
}