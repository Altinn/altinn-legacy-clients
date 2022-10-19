namespace StandAloneNotification;

public interface INotificationClient
{
    Task SendNotification(Notification notification);
}