using System;
using System.Collections.Generic;
using System.Text;
namespace Notification_API.Models.Notification
{
    public enum NotificationType
    {
        FCM = 1,
        WND = 2
    }
    public static class NotificationTemplate
    {
        public static string FcmNotificationContent = "{\"data\":{\"message\": \"{0}\" }}";
        public static string WnsNotification = 
            "<?xml version=\"1.0\" encoding=\"utf-8\"?><toast><visual><binding template=\"ToastText01\"><text id=\"{0}\">" +
            "{1}</text></binding></visual></toast>";
    }
    public static class NotificationMessage
    {
        public static string SetNotification(NotificationType type, string message, int? windowsNotificationId)
        {
            switch (type)
            {
                case NotificationType.FCM:
                    return NotificationTemplate.FcmNotificationContent.Replace("{0}", message);
                case NotificationType.WND:
                    return NotificationTemplate.WnsNotification
                        .Replace("{0}", (windowsNotificationId ?? 1).ToString())
                        .Replace("{1}", message);
                default:
                    return NotificationTemplate.FcmNotificationContent.Replace("{0}", message);
            }
        }
    }
}
