using ZawatSys.MicroLib.Notification.Domain.Entities;

namespace ZawatSys.MicroLib.Notification.Application.Abstractions.Repositories;

/// <summary>
/// Repository contract for NotificationRequest aggregate root.
/// </summary>
public interface INotificationRequestRepository
{
    /// <summary>Creates a new notification request and returns the assigned identifier.</summary>
    Task<Guid> CreateAsync(NotificationRequest request, CancellationToken cancellationToken = default);

    /// <summary>Retrieves a request by its unique identifier.</summary>
    Task<NotificationRequest?> GetByIdAsync(Guid requestId, CancellationToken cancellationToken = default);

    /// <summary>Updates an existing notification request.</summary>
    Task UpdateAsync(NotificationRequest request, CancellationToken cancellationToken = default);

    /// <summary>Retrieves all requests for a given tenant with optional filtering.</summary>
    Task<IReadOnlyList<NotificationRequest>> GetByTenantAsync(
        Guid tenantId,
        NotificationRequestStatus? status = null,
        int skip = 0,
        int take = 50,
        CancellationToken cancellationToken = default);

    /// <summary>Retrieves requests filtered by lifecycle status.</summary>
    Task<IReadOnlyList<NotificationRequest>> GetByStatusAsync(
        NotificationRequestStatus status,
        int skip = 0,
        int take = 50,
        CancellationToken cancellationToken = default);

    /// <summary>Retrieves a request by its tenant-scoped idempotency key to detect duplicates.</summary>
    Task<NotificationRequest?> GetByIdempotencyKeyAsync(
        Guid tenantId,
        string idempotencyKey,
        CancellationToken cancellationToken = default);

    /// <summary>Retrieves all requests for a given correlation identifier across tenants.</summary>
    Task<IReadOnlyList<NotificationRequest>> GetByCorrelationIdAsync(
        string correlationId,
        int skip = 0,
        int take = 50,
        CancellationToken cancellationToken = default);
}
