using Notification_API.Models;
using Notification_API.Models.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notification_API.Service
{
    public interface INotification
    {
        List<NotificationResponse> SendNotification(NotificationRequest request);
        string Registration(DeviceRegistration registration);
        bool DeleteRegistration(string registrationId);
    }
}
