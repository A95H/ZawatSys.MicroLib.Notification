using ZawatSys.MicroLib.Notification.Domain.Enums;

namespace ZawatSys.MicroLib.Notification.Domain.ValueObjects;

/// <summary>
/// Caller-supplied overrides that take precedence over rule defaults.
/// Any non-null property replaces the corresponding rule value.
/// </summary>
public sealed class RuleOverrides
{
    /// <summary>When set, replaces the rule's TemplateKey.</summary>
    public string? TemplateKey { get; init; }

    /// <summary>When non-empty, replaces the rule's DefaultChannels.</summary>
    public IReadOnlyList<NotificationChannelCode>? Channels { get; init; }

    /// <summary>When set, replaces the rule's AudienceResolverKey.</summary>
    public string? AudienceResolverKey { get; init; }
}
