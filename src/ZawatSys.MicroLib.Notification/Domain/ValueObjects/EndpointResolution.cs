using System.Diagnostics.CodeAnalysis;
using ZawatSys.MicroLib.Notification.Domain.Enums;

namespace ZawatSys.MicroLib.Notification.Domain.ValueObjects;

/// <summary>
/// Resolved contact coordinate for delivery: endpoint value, channel, verification status, and preference flags.
/// Retrieved from ContactService at dispatch time; filtered by do-not-contact and preference.
/// </summary>
public sealed class EndpointResolution : IEquatable<EndpointResolution>
{
    public required string Value { get; init; }
    public NotificationChannelCode Channel { get; init; }
    public bool IsVerified { get; init; }
    public bool IsActive { get; init; }

    private EndpointResolution() { } // EF Core

    [SetsRequiredMembers]
    public EndpointResolution(
        string value,
        NotificationChannelCode channel,
        bool isVerified = false,
        bool isActive = true)
    {
        Value = value;
        Channel = channel;
        IsVerified = isVerified;
        IsActive = isActive;
    }

    public bool Equals(EndpointResolution? other)
    {
        if (other is null) return false;
        return Value == other.Value && Channel == other.Channel;
    }

    public override bool Equals(object? obj) => Equals(obj as EndpointResolution);

    public override int GetHashCode() => HashCode.Combine(Value, Channel);
}