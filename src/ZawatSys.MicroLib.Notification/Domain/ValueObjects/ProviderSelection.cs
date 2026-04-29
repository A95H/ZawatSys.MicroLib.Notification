using ZawatSys.MicroLib.Notification.Domain.Enums;

namespace ZawatSys.MicroLib.Notification.Domain.ValueObjects;

/// <summary>
/// Provider selection result with fallback chain for a given channel.
/// </summary>
public sealed class ProviderSelection : IEquatable<ProviderSelection>
{
    public NotificationProviderName SelectedProvider { get; init; }
    public IReadOnlyList<NotificationProviderName> FallbackChain { get; init; } = Array.Empty<NotificationProviderName>();

    private ProviderSelection() { } // EF Core

    public ProviderSelection(
        NotificationProviderName selectedProvider,
        IReadOnlyList<NotificationProviderName>? fallbackChain = null)
    {
        SelectedProvider = selectedProvider;
        FallbackChain = fallbackChain ?? Array.Empty<NotificationProviderName>();
    }

    public bool Equals(ProviderSelection? other)
    {
        if (other is null) return false;
        return SelectedProvider == other.SelectedProvider
            && FallbackChain.SequenceEqual(other.FallbackChain);
    }

    public override bool Equals(object? obj) => Equals(obj as ProviderSelection);

    public override int GetHashCode() => HashCode.Combine(SelectedProvider, FallbackChain.GetHashCode());
}