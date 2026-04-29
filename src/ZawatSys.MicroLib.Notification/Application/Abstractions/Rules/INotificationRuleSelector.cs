using ZawatSys.MicroLib.Notification.Domain.Exceptions;
using ZawatSys.MicroLib.Notification.Domain.ValueObjects;

namespace ZawatSys.MicroLib.Notification.Application.Abstractions.Rules;

/// <summary>
/// Selects the active notification rule for a given event type or explicit trigger key,
/// then merges any caller-supplied overrides using deterministic precedence:
/// explicit override > rule default.
/// </summary>
public interface INotificationRuleSelector
{
    /// <summary>
    /// Resolves the rule and applies overrides.
    /// Throws <see cref="RuleNotFoundException"/> when no active rule exists
    /// and <paramref name="overrides"/> does not supply a fallback template key.
    /// </summary>
    Task<RuleResolutionResult> SelectAsync(
        Guid tenantId,
        string? eventType,
        string? ruleKey,
        RuleOverrides? overrides = null,
        CancellationToken cancellationToken = default);
}
