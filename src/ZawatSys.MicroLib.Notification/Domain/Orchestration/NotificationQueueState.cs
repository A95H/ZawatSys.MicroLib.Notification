namespace ZawatSys.MicroLib.Notification.Domain.Orchestration;

/// <summary>
/// Represents the queue state machine for a notification request.
/// Tracks the high-level request lifecycle state transitions:
/// Pending → Queued → Processing → Completed/Failed/Cancelled
/// </summary>
public sealed class NotificationQueueState
{
    public Guid RequestId { get; }
    public Guid TenantId { get; }
    public QueueState CurrentState { get; private set; }
    public string? FailureReason { get; private set; }
    public DateTimeOffset LastStateChangedAt { get; private set; }
    public DateTimeOffset CreatedAt { get; }

    public NotificationQueueState(Guid requestId, Guid tenantId)
    {
        RequestId = requestId;
        TenantId = tenantId;
        CurrentState = QueueState.Pending;
        CreatedAt = DateTimeOffset.UtcNow;
        LastStateChangedAt = CreatedAt;
    }

    /// <summary>
    /// Transitions the request from Pending to Queued.
    /// Atomic with outbox message publication.
    /// </summary>
    public void MarkQueued()
    {
        TransitionTo(QueueState.Queued);
    }

    /// <summary>
    /// Transitions the request from Queued to Processing.
    /// Driven by dispatch worker pickup.
    /// </summary>
    public void MarkProcessing()
    {
        EnsureStateIsOneOf(QueueState.Queued);
        TransitionTo(QueueState.Processing);
    }

    /// <summary>
    /// Transitions to Paused state (e.g., rate limit backoff or scheduled delay).
    /// </summary>
    public void MarkPaused()
    {
        EnsureStateIsOneOf(QueueState.Processing);
        TransitionTo(QueueState.Paused);
    }

    /// <summary>
    /// Transitions from Paused back to Processing (backoff complete or scheduled time reached).
    /// </summary>
    public void MarkResumed()
    {
        EnsureStateIsOneOf(QueueState.Paused);
        TransitionTo(QueueState.Processing);
    }

    /// <summary>
    /// Marks the request as completed. Terminal state.
    /// Called when all attempts have reached Acknowledged or terminal failure.
    /// </summary>
    public void MarkCompleted()
    {
        TransitionTo(QueueState.Completed);
    }

    /// <summary>
    /// Marks the request as failed. Terminal state.
    /// Called when all retryable attempts have exhausted retries with no success.
    /// </summary>
    public void MarkFailed(string reason)
    {
        FailureReason = reason;
        TransitionTo(QueueState.Failed);
    }

    /// <summary>
    /// Marks the request as cancelled. Terminal state.
    /// </summary>
    public void MarkCancelled()
    {
        EnsureStateIsOneOf(QueueState.Pending, QueueState.Queued, QueueState.Processing);
        TransitionTo(QueueState.Cancelled);
    }

    public bool IsTerminal => CurrentState is QueueState.Completed
                                       or QueueState.Failed
                                       or QueueState.Cancelled;

    public bool IsDispatchEligible => CurrentState is QueueState.Queued
                                                or QueueState.Processing;

    private void TransitionTo(QueueState newState)
    {
        CurrentState = newState;
        LastStateChangedAt = DateTimeOffset.UtcNow;
    }

    private void EnsureStateIsOneOf(params QueueState[] allowed)
    {
        if (!allowed.Contains(CurrentState))
            throw new InvalidOperationException(
                $"Cannot transition from '{CurrentState}' to requested state.");
    }
}

/// <summary>
/// High-level notification request queue states.
/// </summary>
public enum QueueState
{
    /// <summary>Request created, audience resolved, not yet queued.</summary>
    Pending = 0,

    /// <summary>Request queued; outbox message published.</summary>
    Queued = 1,

    /// <summary>At least one attempt is in flight.</summary>
    Processing = 2,

    /// <summary>Backoff or scheduled delay active.</summary>
    Paused = 3,

    /// <summary>All required attempts succeeded or reached terminal state.</summary>
    Completed = 4,

    /// <summary>All attempts exhausted with no success.</summary>
    Failed = 5,

    /// <summary>Explicitly cancelled before completion.</summary>
    Cancelled = 6
}
