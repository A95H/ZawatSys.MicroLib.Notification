using ZawatSys.MicroLib.Notification.Domain.ValueObjects;

namespace ZawatSys.MicroLib.Notification.Application.Abstractions.AudienceResolvers;

/// <summary>
/// Strategy contract for resolving the notification audience from a typed integration event.
/// Each implementation handles one event type and returns a deduplicated list of recipients.
/// </summary>
public interface INotificationAudienceResolver<TEvent>
    where TEvent : class
{
    /// <summary>
    /// Resolves the set of recipient targets for the given event.
    /// Implementations must return a deduplicated collection.
    /// </summary>
    Task<IReadOnlyList<RecipientTarget>> ResolveAsync(
        TEvent @event,
        CancellationToken cancellationToken = default);
}
