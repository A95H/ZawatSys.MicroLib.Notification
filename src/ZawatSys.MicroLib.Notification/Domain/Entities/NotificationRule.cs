namespace ZawatSys.MicroLib.Notification.Domain.Entities;

/// <summary>
/// Minimal MVP rule: associates an event type or trigger key with a template key, default channels, and audience resolver.
/// Stored centrally; not embedded in consumers.
/// </summary>
public sealed class NotificationRule
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }
    public string EventType { get; private set; } = string.Empty;
    public string TemplateKey { get; private set; } = string.Empty;
    public string DefaultChannels { get; private set; } = "[]"; // JSON array of channel codes
    public string AudienceResolverKey { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public string MetadataJson { get; private set; } = "{}";
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    private NotificationRule() { } // EF Core

    public NotificationRule(
        Guid id,
        Guid tenantId,
        string eventType,
        string templateKey,
        string audienceResolverKey,
        bool isActive = true,
        string defaultChannels = "[]",
        string metadataJson = "{}",
        DateTimeOffset? createdAt = null)
    {
        Id = id;
        TenantId = tenantId;
        EventType = eventType;
        TemplateKey = templateKey;
        AudienceResolverKey = audienceResolverKey;
        IsActive = isActive;
        DefaultChannels = defaultChannels;
        MetadataJson = metadataJson;
        CreatedAt = createdAt ?? DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Deactivates this rule.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
    }
}