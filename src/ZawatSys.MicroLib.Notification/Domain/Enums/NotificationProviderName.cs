namespace ZawatSys.MicroLib.Notification.Domain.Enums;

/// <summary>
/// Infrastructure adapter for notification delivery.
/// </summary>
public enum NotificationProviderName
{
    SendGrid = 1,
    Twilio = 2,
    Firebase = 3,
    MetaCloud = 4,
    TelegramBot = 5
}