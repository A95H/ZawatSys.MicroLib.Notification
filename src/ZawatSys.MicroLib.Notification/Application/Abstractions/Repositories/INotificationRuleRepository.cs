using ZawatSys.MicroLib.Notification.Domain.Entities;

namespace ZawatSys.MicroLib.Notification.Application.Abstractions.Repositories;

/// <summary>
/// Repository contract for NotificationRule entities.
/// Minimal MVP rule: associates event type or trigger key with a template key,
/// default channels, and audience resolver key.
/// </summary>
public interface INotificationRuleRepository
{
    /// <summary>Creates a new notification rule record.</summary>
    Task<Guid> CreateAsync(NotificationRule rule, CancellationToken cancellationToken = default);

    /// <summary>Updates an existing notification rule.</summary>
    Task UpdateAsync(NotificationRule rule, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the active rule matching the given event type or trigger key.
    /// Returns the most specific match (rule key beats event type).
    /// </summary>
    Task<NotificationRule?> GetActiveByEventTypeAsync(
        Guid tenantId,
        string? eventType = null,
        string? ruleKey = null,
        CancellationToken cancellationToken = default);

    /// <summary>Retrieves all rules for a given tenant.</summary>
    Task<IReadOnlyList<NotificationRule>> GetByTenantAsync(
        Guid tenantId,
        bool? activeOnly = null,
        int skip = 0,
        int take = 50,
        CancellationToken cancellationToken = default);

    /// <summary>Retrieves a rule by its unique identifier.</summary>
    Task<NotificationRule?> GetByIdAsync(
        Guid ruleId,
        CancellationToken cancellationToken = default);
}
