using ZawatSys.MicroLib.Notification.Domain.Entities;
using ZawatSys.MicroLib.Notification.Domain.ValueObjects;

namespace ZawatSys.MicroLib.Notification.Application.Abstractions.DomainServices;

/// <summary>
/// Defines the boundary for dispatch attempt creation planning.
/// Orchestrates the decision of which recipient-channel-provider tuples to create
/// attempt records for, based on rule configuration and resolved endpoints.
/// </summary>
public interface IDispatchPlanningDomainService
{
    /// <summary>
    /// Plans the set of delivery attempts to create for a given notification request.
    /// Applies rule-based default channels, resolved endpoints, and provider selection
    /// to produce a list of attempt specifications ready for persistence.
    /// </summary>
    /// <param name="request">The notification request to plan attempts for.</param>
    /// <param name="resolvedRecipients">Audience expansion result containing recipient targets.</param>
    /// <param name="endpointsByRecipient">Map of recipientId to their resolved and filtered endpoints.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Ordered list of attempt specifications including recipient, channel, and provider.</returns>
    Task<IReadOnlyList<DispatchAttemptSpecification>> PlanDispatchAttemptsAsync(
        NotificationRequest request,
        IReadOnlyList<RecipientTarget> resolvedRecipients,
        IReadOnlyDictionary<Guid, IReadOnlyList<EndpointResolution>> endpointsByRecipient,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// Specification for a single dispatch attempt to be created.
/// </summary>
public sealed class DispatchAttemptSpecification
{
    public Guid RecipientId { get; init; }
    public string Channel { get; init; } = string.Empty;
    public string ProviderId { get; init; } = string.Empty;
    public Guid ProviderConfigId { get; init; }
    public int AttemptNumber { get; init; } = 1;
    public string? ProviderTemplateKey { get; init; }
}
