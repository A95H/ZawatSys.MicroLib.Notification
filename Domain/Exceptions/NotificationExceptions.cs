using ZawatSys.MicroLib.Notification.MessageCodes;

namespace ZawatSys.MicroLib.Notification.Domain.Exceptions;

/// <summary>
/// Base exception for all Notification domain errors.
/// </summary>
public abstract class NotificationException : Exception
{
    public string ErrorCode { get; }
    public int HttpStatusCode { get; }
    public bool IsRetryable { get; }

    protected NotificationException(string message, string errorCode, int httpStatusCode, bool isRetryable = false)
        : base(message)
    {
        ErrorCode = errorCode;
        HttpStatusCode = httpStatusCode;
        IsRetryable = isRetryable;
    }

    protected NotificationException(string message, string errorCode, int httpStatusCode, Exception innerException, bool isRetryable = false)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
        HttpStatusCode = httpStatusCode;
        IsRetryable = isRetryable;
    }
}

// === Validation Exceptions ===

/// <summary>
/// Interface for exceptions that carry validation errors.
/// </summary>
public interface IHasValidationErrors
{
    IReadOnlyList<string> ValidationErrors { get; }
}

/// <summary>Request fields missing, malformed, or inconsistent.</summary>
public sealed class InvalidRequestException : NotificationException, IHasValidationErrors
{
    public IReadOnlyList<string> ValidationErrors { get; }

    public InvalidRequestException(string message, IReadOnlyList<string>? errors = null)
        : base(message, NotificationErrorCodes.InvalidRequest, 400)
    {
        ValidationErrors = errors ?? Array.Empty<string>();
    }
}

/// <summary>Idempotency key matched an existing request.</summary>
public sealed class DuplicateRequestException : NotificationException
{
    public Guid ExistingRequestId { get; }

    public DuplicateRequestException(Guid existingRequestId, string message = "Request with this idempotency key already exists.")
        : base(message, NotificationErrorCodes.Duplicate, 409)
    {
        ExistingRequestId = existingRequestId;
    }
}

/// <summary>Requested channel not in allowed channel set for tenant.</summary>
public sealed class UnsupportedChannelException : NotificationException
{
    public string Channel { get; }
    public IReadOnlyList<string> AllowedChannels { get; }

    public UnsupportedChannelException(string channel, IReadOnlyList<string>? allowedChannels = null)
        : base($"Channel '{channel}' is not supported.", NotificationErrorCodes.UnsupportedChannel, 400)
    {
        Channel = channel;
        AllowedChannels = allowedChannels ?? Array.Empty<string>();
    }
}

/// <summary>Contract version field missing or malformed.</summary>
public sealed class ContractVersionInvalidException : NotificationException
{
    public string FieldName { get; }

    public ContractVersionInvalidException(string fieldName = "contractVersion")
        : base($"'{fieldName}' field is missing or malformed.", NotificationErrorCodes.ContractVersionInvalid, 400)
    {
        FieldName = fieldName;
    }
}

/// <summary>Contract and payload version mismatch.</summary>
public sealed class ContractVersionMismatchException : NotificationException
{
    public string ContractVersion { get; }
    public string PayloadVersion { get; }

    public ContractVersionMismatchException(string contractVersion, string payloadVersion)
        : base($"Contract version '{contractVersion}' does not match payload version '{payloadVersion}'.", NotificationErrorCodes.ContractVersionMismatch, 400)
    {
        ContractVersion = contractVersion;
        PayloadVersion = payloadVersion;
    }
}

// === Audience Exceptions ===

/// <summary>No recipients could be resolved for given recipientType and context.</summary>
public sealed class AudienceUnresolvedException : NotificationException
{
    public string RecipientType { get; }
    public string? AudienceKey { get; }

    public AudienceUnresolvedException(string recipientType, string? audienceKey = null)
        : base($"No recipients could be resolved for recipientType '{recipientType}'.", NotificationErrorCodes.AudienceUnresolved, 422)
    {
        RecipientType = recipientType;
        AudienceKey = audienceKey;
    }
}

/// <summary>No active notification rule found for the given trigger.</summary>
public sealed class RuleNotFoundException : NotificationException
{
    public string? RuleKey { get; }
    public string? EventType { get; }

    public RuleNotFoundException(string? ruleKey = null, string? eventType = null)
        : base($"No active notification rule found{(ruleKey != null ? $" for key '{ruleKey}'" : "")}{(eventType != null ? $" for event type '{eventType}'" : "")}.", NotificationErrorCodes.RuleNotFound, 422)
    {
        RuleKey = ruleKey;
        EventType = eventType;
    }
}

/// <summary>Rule exists but is inactive and cannot be applied.</summary>
public sealed class RuleInactiveException : NotificationException
{
    public string RuleKey { get; }

    public RuleInactiveException(string ruleKey)
        : base($"Notification rule '{ruleKey}' is inactive.", NotificationErrorCodes.RuleInactive, 422)
    {
        RuleKey = ruleKey;
    }
}

// === Template Exceptions ===

/// <summary>No eligible template revision could be resolved for ResolutionContext.</summary>
public sealed class TemplateUnresolvedException : NotificationException
{
    public string TemplateKey { get; }
    public string? Channel { get; }
    public string? Locale { get; }

    public TemplateUnresolvedException(string templateKey, string? channel = null, string? locale = null)
        : base($"No eligible template found for '{templateKey}'{(channel != null ? $" on channel '{channel}'" : "")}{(locale != null ? $" with locale '{locale}'" : "")}.", NotificationErrorCodes.TemplateUnresolved, 422)
    {
        TemplateKey = templateKey;
        Channel = channel;
        Locale = locale;
    }
}

/// <summary>Template render failed due to invalid variables or rendering error.</summary>
public sealed class TemplateRenderException : NotificationException
{
    public string TemplateKey { get; }
    public string? RenderError { get; }

    public TemplateRenderException(string templateKey, string? renderError = null, Exception? innerException = null)
        : base($"Template render failed for '{templateKey}'{(renderError != null ? $": {renderError}" : "")}.", NotificationErrorCodes.TemplateRenderFailure, 422, innerException)
    {
        TemplateKey = templateKey;
        RenderError = renderError;
    }
}

// === Endpoint Exceptions ===

/// <summary>No viable endpoint could be resolved for any recipient channel.</summary>
public sealed class EndpointUnresolvedException : NotificationException
{
    public string RecipientId { get; }
    public IReadOnlyList<string> RequestedChannels { get; }

    public EndpointUnresolvedException(string recipientId, IReadOnlyList<string>? requestedChannels = null)
        : base($"No viable endpoint could be resolved for recipient '{recipientId}'.", NotificationErrorCodes.EndpointUnresolved, 422)
    {
        RecipientId = recipientId;
        RequestedChannels = requestedChannels ?? Array.Empty<string>();
    }
}

/// <summary>Endpoint was found but is invalid (wrong format, inactive, etc.).</summary>
public sealed class EndpointInvalidException : NotificationException
{
    public string RecipientId { get; }
    public string Channel { get; }
    public string Reason { get; }

    public EndpointInvalidException(string recipientId, string channel, string reason)
        : base($"Endpoint for recipient '{recipientId}' on channel '{channel}' is invalid: {reason}.", NotificationErrorCodes.EndpointInvalid, 422)
    {
        RecipientId = recipientId;
        Channel = channel;
        Reason = reason;
    }
}

// === Provider Exceptions ===

/// <summary>No active provider configuration found for the requested channel.</summary>
public sealed class ProviderConfigNotFoundException : NotificationException
{
    public string Channel { get; }

    public ProviderConfigNotFoundException(string channel)
        : base($"No active provider configuration found for channel '{channel}'.", NotificationErrorCodes.ProviderConfigNotFound, 500, isRetryable: false)
    {
        Channel = channel;
    }
}

/// <summary>Provider configuration is inactive and cannot be used.</summary>
public sealed class ProviderConfigInactiveException : NotificationException
{
    public string Channel {  get; }
    public Guid ConfigId { get; }

    public ProviderConfigInactiveException(string channel, Guid configId)
        : base($"Provider configuration '{configId}' for channel '{channel}' is inactive.", NotificationErrorCodes.ProviderConfigInactive, 500, isRetryable: false)
    {
        Channel = channel;
        ConfigId = configId;
    }
}

/// <summary>Provider template binding not found for binding-required channels.</summary>
public sealed class ProviderBindingNotFoundException : NotificationException
{
    public string Channel { get; }
    public string ProviderId { get; }
    public string TemplateKey { get; }

    public ProviderBindingNotFoundException(string channel, string providerId, string templateKey)
        : base($"No provider template binding found for channel '{channel}', provider '{providerId}', template '{templateKey}'.", NotificationErrorCodes.ProviderBindingNotFound, 500, isRetryable: false)
    {
        Channel = channel;
        ProviderId = providerId;
        TemplateKey = templateKey;
    }
}

/// <summary>Provider bootstrap or initialization failed.</summary>
public sealed class ProviderBootstrapException : NotificationException
{
    public string Channel { get; }
    public string ProviderId { get; }

    public ProviderBootstrapException(string channel, string providerId, string message, Exception? innerException = null)
        : base($"Provider bootstrap failed for channel '{channel}', provider '{providerId}': {message}", NotificationErrorCodes.ProviderBootstrapFailure, 500, innerException, isRetryable: true)
    {
        Channel = channel;
        ProviderId = providerId;
    }
}

/// <summary>Provider execution failed.</summary>
public sealed class ProviderExecutionException : NotificationException
{
    public string Channel { get; }
    public string ProviderId { get; }
    public bool IsRetryable { get; }

    public ProviderExecutionException(string channel, string providerId, string message, bool isRetryable = true, Exception? innerException = null)
        : base($"Provider execution failed for channel '{channel}', provider '{providerId}': {message}", NotificationErrorCodes.ProviderExecutionFailure, isRetryable ? 503 : 500, innerException, isRetryable)
    {
        Channel = channel;
        ProviderId = providerId;
        IsRetryable = isRetryable;
    }
}

/// <summary>All providers in fallback chain have failed.</summary>
public sealed class ProviderFallbackExhaustedException : NotificationException
{
    public string Channel { get; }
    public IReadOnlyList<string> FailedProviders { get; }

    public ProviderFallbackExhaustedException(string channel, IReadOnlyList<string> failedProviders)
        : base($"All providers exhausted for channel '{channel}'. Failed providers: {string.Join(", ", failedProviders)}.", NotificationErrorCodes.ProviderFallbackExhausted, 500, isRetryable: false)
    {
        Channel = channel;
        FailedProviders = failedProviders;
    }
}

// === Authorization/Authentication Exceptions ===

/// <summary>Caller cannot send notifications in the requested tenant scope.</summary>
public sealed class TenantScopeDeniedException : NotificationException
{
    public Guid RequestedTenantId { get; }
    public Guid? CallerTenantId { get; }

    public TenantScopeDeniedException(Guid requestedTenantId, Guid? callerTenantId = null)
        : base($"Caller does not have access to tenant scope '{requestedTenantId}'.", NotificationErrorCodes.TenantScopeDenied, 403)
    {
        RequestedTenantId = requestedTenantId;
        CallerTenantId = callerTenantId;
    }
}

/// <summary>Caller does not have permission for the requested operation.</summary>
public sealed class AuthorizationDeniedException : NotificationException
{
    public string RequiredPermission { get; }

    public AuthorizationDeniedException(string requiredPermission)
        : base($"Caller does not have the required permission: '{requiredPermission}'.", NotificationErrorCodes.AuthorizationDenied, 403)
    {
        RequiredPermission = requiredPermission;
    }
}

/// <summary>Caller is unauthenticated on the send surface.</summary>
public sealed class AuthenticationRequiredException : NotificationException
{
    public AuthenticationRequiredException(string message = "Authentication is required to access this resource.")
        : base(message, NotificationErrorCodes.AuthenticationRequired, 401)
    {
    }
}

// === Internal Exceptions ===

/// <summary>Unexpected internal system error.</summary>
public sealed class InternalErrorException : NotificationException
{
    public InternalErrorException(string message = "An unexpected internal error occurred.", Exception? innerException = null)
        : base(message, NotificationErrorCodes.InternalError, 500, innerException)
    {
    }
}

/// <summary>Database or persistence operation failed.</summary>
public sealed class PersistenceException : NotificationException
{
    public PersistenceException(string message, Exception? innerException = null)
        : base(message, NotificationErrorCodes.PersistenceFailure, 500, innerException)
    {
    }
}

/// <summary>Message broker or outbox operation failed.</summary>
public sealed class MessagingException : NotificationException
{
    public MessagingException(string message, Exception? innerException = null)
        : base(message, NotificationErrorCodes.MessagingFailure, 500, innerException)
    {
    }
}

// Helper to access StatusCodes withoutMicrosoft.AspNetCore.Http namespace reference in domain
file static class StatusCodes
{
    public const int Status400BadRequest = 400;
    public const int Status401Unauthorized = 401;
    public const int Status403Forbidden = 403;
    public const int Status409Conflict = 409;
    public const int Status422UnprocessableEntity = 422;
    public const int Status500InternalServerError = 500;
    public const int Status503ServiceUnavailable = 503;
}
