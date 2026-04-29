namespace ZawatSys.MicroLib.Notification.Domain.Orchestration;

/// <summary>
/// Represents the dispatch planning state machine for a notification request.
/// Tracks which recipients and channels have been planned, attempted, and completed.
/// This state is used by dispatch workers to drive attempt creation and retry logic.
/// </summary>
public sealed class NotificationDispatchState
{
    private readonly Dictionary<Guid, RecipientDispatchTracker> _recipientTrackers = new();

    public Guid RequestId { get; }
    public Guid TenantId { get; }
    public string CorrelationId { get; } = string.Empty;

    public NotificationDispatchState(Guid requestId, Guid tenantId, string correlationId)
    {
        RequestId = requestId;
        TenantId = tenantId;
        CorrelationId = correlationId;
    }

    /// <summary>
    /// Marks a recipient-channel tuple as planned for dispatch attempt creation.
    /// </summary>
    public void MarkRecipientChannelPlanned(Guid recipientId, string channel)
    {
        if (!_recipientTrackers.TryGetValue(recipientId, out var tracker))
        {
            tracker = new RecipientDispatchTracker(recipientId);
            _recipientTrackers[recipientId] = tracker;
        }
        tracker.MarkChannelPlanned(channel);
    }

    /// <summary>
    /// Marks a recipient-channel tuple as dispatched (attempt created).
    /// </summary>
    public void MarkRecipientChannelDispatched(Guid recipientId, string channel, Guid attemptId)
    {
        if (_recipientTrackers.TryGetValue(recipientId, out var tracker))
        {
            tracker.MarkChannelDispatched(channel, attemptId);
        }
    }

    /// <summary>
    /// Marks a recipient-channel tuple as completed (terminal state reached).
    /// </summary>
    public void MarkRecipientChannelCompleted(Guid recipientId, string channel)
    {
        if (_recipientTrackers.TryGetValue(recipientId, out var tracker))
        {
            tracker.MarkChannelCompleted(channel);
        }
    }

    /// <summary>
    /// Returns true if all planned recipient-channel tuples have reached terminal state.
    /// </summary>
    public bool IsAllRecipientChannelCompleted()
    {
        return _recipientTrackers.Values.All(t => t.IsCompleted());
    }

    /// <summary>
    /// Returns the count of recipients still awaiting completion.
    /// </summary>
    public int GetPendingRecipientCount()
    {
        return _recipientTrackers.Values.Count(t => !t.IsCompleted());
    }

    /// <summary>
    /// Returns the full set of tracked recipients.
    /// </summary>
    public IReadOnlyCollection<RecipientDispatchTracker> GetAllRecipients()
    {
        return _recipientTrackers.Values.ToList().AsReadOnly();
    }

    /// <summary>
    /// Per-recipient dispatch tracker.
    /// </summary>
    public sealed class RecipientDispatchTracker
    {
        public Guid RecipientId { get; }
        private readonly Dictionary<string, ChannelDispatchStatus> _channelStatuses = new();

        public RecipientDispatchTracker(Guid recipientId)
        {
            RecipientId = recipientId;
        }

        public void MarkChannelPlanned(string channel)
        {
            if (!_channelStatuses.ContainsKey(channel))
            {
                _channelStatuses[channel] = new ChannelDispatchStatus(channel);
            }
        }

        public void MarkChannelDispatched(string channel, Guid attemptId)
        {
            if (_channelStatuses.TryGetValue(channel, out var status))
            {
                status.MarkDispatched(attemptId);
            }
        }

        public void MarkChannelCompleted(string channel)
        {
            if (_channelStatuses.TryGetValue(channel, out var status))
            {
                status.MarkCompleted();
            }
        }

        public bool IsCompleted() => _channelStatuses.Values.All(s => s.IsTerminal);

        public IReadOnlyDictionary<string, ChannelDispatchStatus> GetChannelStatuses()
            => _channelStatuses;
    }

    /// <summary>
    /// Per-channel dispatch status within a recipient.
    /// </summary>
    public sealed class ChannelDispatchStatus
    {
        public string Channel { get; }
        public bool IsPlanned { get; private set; }
        public bool IsDispatched { get; private set; }
        public Guid? CurrentAttemptId { get; private set; }
        public bool IsTerminal { get; private set; }

        public ChannelDispatchStatus(string channel)
        {
            Channel = channel;
        }

        public void MarkDispatched(Guid attemptId)
        {
            IsDispatched = true;
            CurrentAttemptId = attemptId;
        }

        public void MarkCompleted()
        {
            IsTerminal = true;
        }
    }
}
