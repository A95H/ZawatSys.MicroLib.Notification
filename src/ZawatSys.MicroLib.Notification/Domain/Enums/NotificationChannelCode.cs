namespace ZawatSys.MicroLib.Notification.Domain.Enums;

/// <summary>
/// Delivery surface for notifications.
/// </summary>
public enum NotificationChannelCode
{
    Email = 1,
    Sms = 2,
    WhatsApp = 3,
    Telegram = 4,
    Push = 5,
    InApp = 6
}