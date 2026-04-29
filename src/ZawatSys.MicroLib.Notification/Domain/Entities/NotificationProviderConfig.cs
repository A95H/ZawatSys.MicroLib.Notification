using ZawatSys.MicroLib.Notification.Domain.Enums;

namespace ZawatSys.MicroLib.Notification.Domain.Entities;

/// <summary>
/// Provider configuration per channel/tenant.
/// Stores ConfigJson (non-sensitive) and SecretsJson (AES-256 encrypted) with KeyVersion for rotation.
/// </summary>
public sealed class NotificationProviderConfig
{
    public Guid Id { get; private set; }
    public Guid TenantId { get; private set; }
    public NotificationChannelCode Channel { get; private set; }
    public NotificationProviderName ProviderName { get; private set; }
    public int Priority { get; private set; }
    public bool IsActive { get; private set; }
    public string ConfigJson { get; private set; } = "{}";
    public string SecretsJson { get; private set; } = "{}"; // AES-256 encrypted
    public int KeyVersion { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private NotificationProviderConfig() { } // EF Core

    public NotificationProviderConfig(
        Guid id,
        Guid tenantId,
        NotificationChannelCode channel,
        NotificationProviderName providerName,
        int priority,
        bool isActive = true,
        string configJson = "{}",
        string secretsJson = "{}",
        int keyVersion = 1)
    {
        Id = id;
        TenantId = tenantId;
        Channel = channel;
        ProviderName = providerName;
        Priority = priority;
        IsActive = isActive;
        ConfigJson = configJson;
        SecretsJson = secretsJson;
        KeyVersion = keyVersion;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Deactivates this provider config.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates configuration and bumps key version for rotation.
    /// </summary>
    public void UpdateConfig(string configJson, string secretsJson, int newKeyVersion)
    {
        ConfigJson = configJson;
        SecretsJson = secretsJson;
        KeyVersion = newKeyVersion;
        UpdatedAt = DateTime.UtcNow;
    }
}