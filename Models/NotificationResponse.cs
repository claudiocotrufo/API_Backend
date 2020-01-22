using System;
using System.Collections.Generic;
using System.Text;

namespace Notification_API.Models.Notification
{
    public class NotificationResponse
    {
        public NotificationType Type { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
