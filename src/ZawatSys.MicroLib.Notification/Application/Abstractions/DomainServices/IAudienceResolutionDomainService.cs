using ZawatSys.MicroLib.Notification.Domain.ValueObjects;

namespace ZawatSys.MicroLib.Notification.Application.Abstractions.DomainServices;

/// <summary>
/// Defines the boundary for audience resolution orchestration.
/// Implementations expand a logical audience (role, relation, explicit list, etc.)
/// into a collection of resolved recipient targets.
/// </summary>
public interface IAudienceResolutionDomainService
{
    /// <summary>
    /// Resolves the full set of recipient targets for the given audience specification.
    /// </summary>
    /// <param name="tenantId">Tenant context for the resolution.</param>
    /// <param name="audienceKey">The audience resolver key identifying which resolver to invoke.</param>
    /// <param name="audiencePayload">Additional context required by the resolver (event data, explicit user list, etc.).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Immutable snapshot of resolved recipients with their target identities.</returns>
    Task<IReadOnlyList<RecipientTarget>> ResolveAudienceAsync(
        Guid tenantId,
        AudienceResolverKey audienceKey,
        AudienceResolutionContext audiencePayload,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// Stable identifier for an audience resolver implementation.
/// </summary>
public sealed class AudienceResolverKey : IEquatable<AudienceResolverKey>
{
    public string Value { get; }

    public AudienceResolverKey(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Audience resolver key cannot be null or whitespace.", nameof(value));
        Value = value;
    }

    public static AudienceResolverKey RoleBased => new("RoleBased");
    public static AudienceResolverKey RelationBased => new("RelationBased");
    public static AudienceResolverKey Explicit => new("Explicit");
    public static AudienceResolverKey Custom => new("Custom");

    public bool Equals(AudienceResolverKey? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is AudienceResolverKey other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value;
}

/// <summary>
/// Context passed to audience resolvers containing the event or caller data
/// needed to expand the audience.
/// </summary>
public sealed class AudienceResolutionContext
{
    public Guid TenantId { get; init; }
    public string? EventType { get; init; }
    public string? CorrelationId { get; init; }
    public string? TraceId { get; init; }
    public IReadOnlyDictionary<string, string> Metadata { get; init; } = null!;
}
