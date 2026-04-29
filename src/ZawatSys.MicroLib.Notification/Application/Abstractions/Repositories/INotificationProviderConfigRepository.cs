using ZawatSys.MicroLib.Notification.Domain.Entities;
using ZawatSys.MicroLib.Notification.Domain.Enums;

namespace ZawatSys.MicroLib.Notification.Application.Abstractions.Repositories;

/// <summary>
/// Repository contract for NotificationProviderConfig entities.
/// Per-tenant, per-channel provider configuration including priority,
/// active status, and encrypted secrets.
/// </summary>
public interface INotificationProviderConfigRepository
{
    /// <summary>Creates a new provider configuration record.</summary>
    Task<Guid> CreateAsync(NotificationProviderConfig config, CancellationToken cancellationToken = default);

    /// <summary>Updates an existing provider configuration.</summary>
    Task UpdateAsync(NotificationProviderConfig config, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the active provider configuration for a given tenant and channel,
    /// ordered by priority (ascending). Returns the highest-priority (lowest number) active config.
    /// </summary>
    Task<NotificationProviderConfig?> GetActiveByTenantAndChannelAsync(
        Guid tenantId,
        NotificationChannelCode channel,
        CancellationToken cancellationToken = default);

    /// <summary>Retrieves all active provider configurations for a given tenant.</summary>
    Task<IReadOnlyList<NotificationProviderConfig>> GetActiveByTenantAsync(
        Guid tenantId,
        NotificationChannelCode? channel = null,
        CancellationToken cancellationToken = default);

    /// <summary>Retrieves a specific configuration by its unique identifier.</summary>
    Task<NotificationProviderConfig?> GetByIdAsync(
        Guid configId,
        CancellationToken cancellationToken = default);
}
