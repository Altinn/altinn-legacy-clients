using StandAloneNotification.Models;

namespace StandAloneNotification;

public interface INotificationClient
{
    Task SendNotification(List<Notification> notificationList);
}