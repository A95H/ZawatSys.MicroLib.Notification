using ZawatSys.MicroLib.Notification.Application.Abstractions.DomainServices;

namespace ZawatSys.MicroLib.Notification.Application.Abstractions.Mappers;

/// <summary>
/// Maps an inbound integration event to an <see cref="AudienceResolutionContext"/>
/// so that audience resolvers can operate without coupling to the event type.
/// </summary>
public interface INotificationEventMapper<TEvent>
    where TEvent : class
{
    /// <summary>
    /// Extracts the tenant identifier from the event.
    /// </summary>
    Guid GetTenantId(TEvent @event);

    /// <summary>
    /// Extracts the audience resolver key that should handle this event.
    /// </summary>
    string GetAudienceResolverKey(TEvent @event);

    /// <summary>
    /// Extracts the template key to use for the notification.
    /// </summary>
    string GetTemplateKey(TEvent @event);

    /// <summary>
    /// Builds the <see cref="AudienceResolutionContext"/> from the event.
    /// </summary>
    AudienceResolutionContext ToAudienceResolutionContext(TEvent @event);
}
