using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notification_API.Models
{
    public class DeviceRegistration
    {
        public Notification.NotificationType Type { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string RegistationId { get; set; }
        public string Handle { get; set; }
    }
}
