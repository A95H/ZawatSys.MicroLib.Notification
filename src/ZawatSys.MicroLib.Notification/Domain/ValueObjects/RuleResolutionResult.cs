using ZawatSys.MicroLib.Notification.Domain.Enums;

namespace ZawatSys.MicroLib.Notification.Domain.ValueObjects;

/// <summary>
/// Immutable result of rule selection: the resolved template key, channels,
/// and audience resolver key after override precedence has been applied.
/// </summary>
public sealed class RuleResolutionResult
{
    public Guid? RuleId { get; }
    public string TemplateKey { get; }
    public IReadOnlyList<NotificationChannelCode> Channels { get; }
    public string AudienceResolverKey { get; }
    public bool ResolvedFromRule { get; }

    public RuleResolutionResult(
        Guid? ruleId,
        string templateKey,
        IReadOnlyList<NotificationChannelCode> channels,
        string audienceResolverKey,
        bool resolvedFromRule)
    {
        RuleId = ruleId;
        TemplateKey = templateKey;
        Channels = channels;
        AudienceResolverKey = audienceResolverKey;
        ResolvedFromRule = resolvedFromRule;
    }
}
