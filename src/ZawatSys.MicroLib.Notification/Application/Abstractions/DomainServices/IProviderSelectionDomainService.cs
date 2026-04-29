namespace ZawatSys.MicroLib.Notification.Application.Abstractions.DomainServices;

/// <summary>
/// Defines the boundary for provider selection and fallback planning.
/// Implementations select an active provider for a given channel and tenant
/// based on priority, and prepare the fallback chain for retry scenarios.
/// </summary>
public interface IProviderSelectionDomainService
{
    /// <summary>
    /// Selects the active provider for the given channel and tenant.
    /// </summary>
    /// <param name="tenantId">Tenant context.</param>
    /// <param name="channel">The delivery channel (Email, Sms, WhatsApp, etc.).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The selected provider configuration, or null if no active provider is available.</returns>
    Task<ProviderSelectionResult?> SelectActiveProviderAsync(
        Guid tenantId,
        string channel,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the next fallback provider in the chain for the given channel and tenant,
    /// excluding any providers already attempted.
    /// </summary>
    /// <param name="tenantId">Tenant context.</param>
    /// <param name="channel">The delivery channel.</param>
    /// <param name="excludedProviderIds">Providers already attempted in the current chain.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The next fallback provider, or null if the chain is exhausted.</returns>
    Task<ProviderSelectionResult?> SelectNextFallbackProviderAsync(
        Guid tenantId,
        string channel,
        IReadOnlyList<string> excludedProviderIds,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// Result of a provider selection operation.
/// </summary>
public sealed class ProviderSelectionResult
{
    public Guid ConfigId { get; init; }
    public string ProviderId { get; init; } = string.Empty;
    public string Channel { get; init; } = string.Empty;
    public int Priority { get; init; }
    public bool IsFallback { get; init; }
}
