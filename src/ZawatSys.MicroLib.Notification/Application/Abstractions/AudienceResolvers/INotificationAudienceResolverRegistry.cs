using ZawatSys.MicroLib.Notification.Domain.ValueObjects;

namespace ZawatSys.MicroLib.Notification.Application.Abstractions.AudienceResolvers;

/// <summary>
/// Registry that discovers and dispatches to the correct audience resolver
/// for a given event type at runtime.
/// </summary>
public interface INotificationAudienceResolverRegistry
{
    /// <summary>
    /// Resolves the audience for the given event by locating the registered
    /// <see cref="INotificationAudienceResolver{TEvent}"/> and invoking it.
    /// Returns a deduplicated list of recipient targets.
    /// </summary>
    Task<IReadOnlyList<RecipientTarget>> ResolveAsync<TEvent>(
        TEvent @event,
        CancellationToken cancellationToken = default)
        where TEvent : class;

    /// <summary>
    /// Returns true when a resolver is registered for the given event type.
    /// </summary>
    bool HasResolver<TEvent>() where TEvent : class;
}
