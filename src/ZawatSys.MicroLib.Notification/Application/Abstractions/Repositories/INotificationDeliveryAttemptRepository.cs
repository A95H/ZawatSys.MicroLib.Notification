using ZawatSys.MicroLib.Notification.Domain.Entities;
using ZawatSys.MicroLib.Notification.Domain.Enums;

namespace ZawatSys.MicroLib.Notification.Application.Abstractions.Repositories;

/// <summary>
/// Repository contract for NotificationDeliveryAttempt entities.
/// Each attempt is scoped to a parent NotificationRequest and represents
/// a single delivery execution through a specific channel and provider.
/// </summary>
public interface INotificationDeliveryAttemptRepository
{
    /// <summary>Creates a new delivery attempt record and returns the assigned identifier.</summary>
    Task<Guid> CreateAsync(NotificationDeliveryAttempt attempt, CancellationToken cancellationToken = default);

    /// <summary>Retrieves a delivery attempt by its unique identifier.</summary>
    Task<NotificationDeliveryAttempt?> GetByIdAsync(Guid attemptId, CancellationToken cancellationToken = default);

    /// <summary>Updates an existing delivery attempt.</summary>
    Task UpdateAsync(NotificationDeliveryAttempt attempt, CancellationToken cancellationToken = default);

    /// <summary>Retrieves all attempts for a given notification request.</summary>
    Task<IReadOnlyList<NotificationDeliveryAttempt>> GetByRequestIdAsync(
        Guid requestId,
        CancellationToken cancellationToken = default);

    /// <summary>Retrieves a specific attempt by its composite identity.</summary>
    Task<NotificationDeliveryAttempt?> GetByIdentityAsync(
        Guid requestId,
        Guid recipientId,
        NotificationChannelCode channel,
        NotificationProviderName providerId,
        CancellationToken cancellationToken = default);

    /// <summary>Retrieves all retryable attempts that are due for a retry attempt.</summary>
    Task<IReadOnlyList<NotificationDeliveryAttempt>> GetPendingRetriesAsync(
        DateTimeOffset asOf,
        int take = 100,
        CancellationToken cancellationToken = default);

    /// <summary>Retrieves all attempts for a specific recipient within a request.</summary>
    Task<IReadOnlyList<NotificationDeliveryAttempt>> GetByRecipientAsync(
        Guid requestId,
        Guid recipientId,
        CancellationToken cancellationToken = default);
}
