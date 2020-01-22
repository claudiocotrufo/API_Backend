using Microsoft.Azure.NotificationHubs;
using Microsoft.Extensions.Configuration;
using Notification_API.Models;
using Notification_API.Models.Notification;
using System;
using System.Collections.Generic;

namespace Notification_API.Service
{
    public class Notification : INotification
    {
        private IConfiguration _config { get; }
        private NotificationHubClient _hub;

        public Notification(IConfiguration config)
        {
            _config = config;
            _hub = NotificationHubClient.CreateClientFromConnectionString(
                    _config["NotificationHub:hubConnectionString"],
                    _config["NotificationHub:hubName"]);

        }

        public List<NotificationResponse> SendNotification(NotificationRequest request)
        {
            List<NotificationResponse> response = new List<NotificationResponse>();

            if (request.Types != null)
            {
                foreach (var type in request.Types)
                {
                    NotificationOutcome send = new NotificationOutcome();

                    string notification =
                        NotificationMessage.SetNotification(type, request.Message, request.WindowsNotificationId);

                    Exception e = null;

                    try
                    {
                        switch (type)
                        {
                            case NotificationType.FCM:
                                send = _hub.SendFcmNativeNotificationAsync(notification).Result;
                                break;
                            case NotificationType.WND:
                                send = _hub.SendWindowsNativeNotificationAsync(notification).Result;
                                break;
                            default:
                                send = _hub.SendFcmNativeNotificationAsync(notification).Result;
                                break;
                        }

                    }
                    catch (Exception ex)
                    {
                        e = ex;

                        continue;
                    }
                    finally
                    {
                        response.Add(new NotificationResponse
                        {
                            Type = type,
                            Success = (e == null),
                            Error = e?.Message
                        });
                    }
                }
            }


            return response;
        }

        public string Registration(DeviceRegistration registration)
        {
            return  _hub.CreateRegistrationAsync(GetNativeRegistration(registration)).Result;
        }


        public bool DeleteRegistration(string registrationId)
        {
            bool deleted = true;

            try
            {
                _hub.DeleteRegistrationAsync(registrationId).Wait();
            }
            catch
            {
                deleted = false;
            }

            return deleted;
        }

        private dynamic GetNativeRegistration(DeviceRegistration registration)
        {
            switch (registration.Type)
            {
                case NotificationType.FCM:
                    return _hub.CreateFcmNativeRegistrationAsync(registration.Handle).Result;
                case NotificationType.WND:
                    return _hub.CreateWindowsNativeRegistrationAsync(registration.Handle).Result;
                default:
                    return _hub.CreateFcmNativeRegistrationAsync(registration.Handle).Result;
            }
        }
    }
}
