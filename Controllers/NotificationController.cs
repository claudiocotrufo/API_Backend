using Microsoft.AspNetCore.Mvc;
using Notification_API.Models;
using Notification_API.Models.Notification;
using Notification_API.Service;
using System.Collections.Generic;

namespace Notification_API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotification _notification;

        public NotificationController(INotification notification)
        {
            _notification = notification;
        }

        [HttpPost]
        public List<NotificationResponse> SendNotification(NotificationRequest notification)
        {
            var result = _notification.SendNotification(notification);
            return result;
        }
        [HttpPost]
        public string Register(DeviceRegistration registration)
        {
            var result = _notification.Registration(registration);
            return result;
        }

        [HttpGet]
        public bool DeleteRegistration(string registrationId)
        {
            var result = _notification.DeleteRegistration(registrationId);
            return result;
        }
    }
}
