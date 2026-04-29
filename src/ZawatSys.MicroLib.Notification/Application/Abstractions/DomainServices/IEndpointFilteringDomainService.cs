using ZawatSys.MicroLib.Notification.Domain.ValueObjects;

namespace ZawatSys.MicroLib.Notification.Application.Abstractions.DomainServices;

/// <summary>
/// Defines the boundary for channel preference and consent filtering.
/// Implementations apply do-not-contact flags, channel preferences,
/// and endpoint verification status before dispatch planning.
/// </summary>
public interface IEndpointFilteringDomainService
{
    /// <summary>
    /// Filters a collection of resolved endpoints against tenant and recipient
    /// preferences, consent flags, and verification status.
    /// </summary>
    /// <param name="tenantId">Tenant context for filtering.</param>
    /// <param name="recipientId">The recipient whose endpoints are being filtered.</param>
    /// <param name="endpoints">The collection of candidate endpoints to evaluate.</param>
    /// <param name="requestedChannels">Optional list of channels explicitly requested by the caller.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Filtered and ordered list of eligible endpoints for delivery.</returns>
    Task<IReadOnlyList<EndpointResolution>> FilterEndpointsAsync(
        Guid tenantId,
        Guid recipientId,
        IReadOnlyList<EndpointResolution> endpoints,
        IReadOnlyList<string>? requestedChannels = null,
        CancellationToken cancellationToken = default);
}
