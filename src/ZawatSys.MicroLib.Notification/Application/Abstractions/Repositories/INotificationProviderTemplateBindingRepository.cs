using ZawatSys.MicroLib.Notification.Domain.Entities;
using ZawatSys.MicroLib.Notification.Domain.Enums;

namespace ZawatSys.MicroLib.Notification.Application.Abstractions.Repositories;

/// <summary>
/// Repository contract for NotificationProviderTemplateBinding entities.
/// Maps provider, channel, and generic template key to provider-specific
/// approved template identifiers required by WhatsApp, Telegram, etc.
/// </summary>
public interface INotificationProviderTemplateBindingRepository
{
    /// <summary>Creates a new provider template binding record.</summary>
    Task<Guid> CreateAsync(NotificationProviderTemplateBinding binding, CancellationToken cancellationToken = default);

    /// <summary>Updates an existing provider template binding record.</summary>
    Task UpdateAsync(NotificationProviderTemplateBinding binding, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the active binding for a specific provider, channel, and template key.
    /// </summary>
    Task<NotificationProviderTemplateBinding?> GetActiveBindingAsync(
        Guid tenantId,
        NotificationProviderName providerId,
        NotificationChannelCode channel,
        string templateKey,
        CancellationToken cancellationToken = default);

    /// <summary>Retrieves all bindings for a given tenant.</summary>
    Task<IReadOnlyList<NotificationProviderTemplateBinding>> GetByTenantAsync(
        Guid tenantId,
        NotificationChannelCode? channel = null,
        NotificationProviderName? providerId = null,
        CancellationToken cancellationToken = default);

    /// <summary>Retrieves a binding by its unique identifier.</summary>
    Task<NotificationProviderTemplateBinding?> GetByIdAsync(
        Guid bindingId,
        CancellationToken cancellationToken = default);
}
