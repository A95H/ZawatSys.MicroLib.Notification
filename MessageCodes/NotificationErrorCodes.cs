namespace ZawatSys.MicroLib.Notification.MessageCodes;

/// <summary>
/// Stable machine-readable error codes for NotificationService.
/// These codes are used in API error envelopes, request-reply error responses,
/// and worker-path failure diagnostics.
///
/// Categories:
/// - Validation (4xx): Invalid request, missing data, contract version issues
/// - Audience (422): Unresolved audience, missing rule
/// - Template (422): Template resolution failures
/// - Provider (5xx): Provider configuration or execution failures
/// - Authorization (403): Tenant scope or permission violations
/// - Authentication (401): Unauthenticated access
/// - Internal (500): Unexpected system errors
/// </summary>
public static class NotificationErrorCodes
{
    // === Validation Errors (400) ===

    /// <summary>Request fields missing, malformed, or inconsistent.</summary>
    public const string InvalidRequest = "NOTIFICATION_INVALID_REQUEST";

    /// <summary>Idempotency key matched an existing request.</summary>
    public const string Duplicate = "NOTIFICATION_DUPLICATE";

    /// <summary>Requested channel not in allowed channel set for tenant.</summary>
    public const string UnsupportedChannel = "NOTIFICATION_UNSUPPORTED_CHANNEL";

    /// <summary>Contract version field missing or malformed.</summary>
    public const string ContractVersionInvalid = "NOTIFICATION_CONTRACT_VERSION_INVALID";

    /// <summary>Contract and payload version mismatch.</summary>
    public const string ContractVersionMismatch = "NOTIFICATION_CONTRACT_VERSION_MISMATCH";

    // === Audience Errors (422) ===

    /// <summary>No recipients could be resolved for given recipientType and context.</summary>
    public const string AudienceUnresolved = "NOTIFICATION_AUDIENCE_UNRESOLVED";

    /// <summary>No active notification rule found for the given trigger.</summary>
    public const string RuleNotFound = "NOTIFICATION_RULE_NOT_FOUND";

    /// <summary>Rule exists but is inactive and cannot be applied.</summary>
    public const string RuleInactive = "NOTIFICATION_RULE_INACTIVE";

    // === Template Errors (422) ===

    /// <summary>No eligible template revision could be resolved for ResolutionContext.</summary>
    public const string TemplateUnresolved = "NOTIFICATION_TEMPLATE_UNRESOLVED";

    /// <summary>Template render failed due to invalid variables or rendering error.</summary>
    public const string TemplateRenderFailure = "NOTIFICATION_TEMPLATE_RENDER_FAILURE";

    // === Endpoint Errors (422) ===

    /// <summary>No viable endpoint could be resolved for any recipient channel.</summary>
    public const string EndpointUnresolved = "NOTIFICATION_ENDPOINT_UNRESOLVED";

    /// <summary>Endpoint was found but is invalid (wrong format, inactive, etc.).</summary>
    public const string EndpointInvalid = "NOTIFICATION_ENDPOINT_INVALID";

    // === Provider Errors (5xx) ===

    /// <summary>No active provider configuration found for the requested channel.</summary>
    public const string ProviderConfigNotFound = "NOTIFICATION_PROVIDER_CONFIG_NOT_FOUND";

    /// <summary>Provider configuration is inactive and cannot be used.</summary>
    public const string ProviderConfigInactive = "NOTIFICATION_PROVIDER_CONFIG_INACTIVE";

    /// <summary>Provider template binding not found for binding-required channels.</summary>
    public const string ProviderBindingNotFound = "NOTIFICATION_PROVIDER_BINDING_NOT_FOUND";

    /// <summary>Provider bootstrap or initialization failed.</summary>
    public const string ProviderBootstrapFailure = "NOTIFICATION_PROVIDER_BOOTSTRAP_FAILURE";

    /// <summary>Provider execution failed (retryable or terminal depending on error type).</summary>
    public const string ProviderExecutionFailure = "NOTIFICATION_PROVIDER_EXECUTION_FAILURE";

    /// <summary>All providers in fallback chain have failed.</summary>
    public const string ProviderFallbackExhausted = "NOTIFICATION_PROVIDER_FALLBACK_EXHAUSTED";

    // === Authorization Errors (403) ===

    /// <summary>Caller cannot send notifications in the requested tenant scope.</summary>
    public const string TenantScopeDenied = "NOTIFICATION_TENANT_SCOPE_DENIED";

    /// <summary>Caller does not have permission for the requested operation.</summary>
    public const string AuthorizationDenied = "NOTIFICATION_AUTHORIZATION_DENIED";

    // === Authentication Errors (401) ===

    /// <summary>Caller is unauthenticated on the send surface.</summary>
    public const string AuthenticationRequired = "NOTIFICATION_AUTHENTICATION_REQUIRED";

    // === Internal Errors (500) ===

    /// <summary>Unexpected internal system error.</summary>
    public const string InternalError = "NOTIFICATION_INTERNAL_ERROR";

    /// <summary>Database or persistence operation failed.</summary>
    public const string PersistenceFailure = "NOTIFICATION_PERSISTENCE_FAILURE";

    /// <summary>Message broker or outbox operation failed.</summary>
    public const string MessagingFailure = "NOTIFICATION_MESSAGING_FAILURE";
}
