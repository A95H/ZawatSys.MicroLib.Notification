using ZawatSys.MicroLib.Notification.Domain.Enums;

namespace ZawatSys.MicroLib.Notification.Domain.Entities;

/// <summary>
/// Provider-approved template bindings for channels that require provider-specific template IDs (WhatsApp, Telegram).
/// Owned by NotificationService — NOT a first-class TemplateService resolution axis.
/// </summary>
public sealed class NotificationProviderTemplateBinding
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }
    public NotificationChannelCode Channel { get; private set; }
    public NotificationProviderName ProviderName { get; private set; }
    public string TemplateKey { get; private set; } = string.Empty;
    public string ProviderTemplateId { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public string MetadataJson { get; private set; } = "{}";

    private NotificationProviderTemplateBinding() { } // EF Core

    public NotificationProviderTemplateBinding(
        Guid id,
        Guid tenantId,
        NotificationChannelCode channel,
        NotificationProviderName providerName,
        string templateKey,
        string providerTemplateId,
        bool isActive = true,
        string metadataJson = "{}")
    {
        Id = id;
        TenantId = tenantId;
        Channel = channel;
        ProviderName = providerName;
        TemplateKey = templateKey;
        ProviderTemplateId = providerTemplateId;
        IsActive = isActive;
        MetadataJson = metadataJson;
    }

    /// <summary>
    /// Deactivates this binding.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
    }
}