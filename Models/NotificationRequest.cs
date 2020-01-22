using System;
using System.Collections.Generic;
using System.Text;

namespace Notification_API.Models.Notification
{
    public class NotificationRequest
    {
        public List<NotificationType> Types {get;set;}
        public string Tags { get; set; }
        public string Message { get; set; }
        public int? WindowsNotificationId { get; set; }

    }
}
