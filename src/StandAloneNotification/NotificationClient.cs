using System.ServiceModel;
using System.ServiceModel.Channels;

using AltinnII.Services.Notification;

namespace StandAloneNotification;

public class NotificationClient : INotificationClient
{
    private readonly NotificationSettings _notificationSettings;

    public NotificationClient(NotificationSettings notificationSettings)
    {
        _notificationSettings = notificationSettings;
    }

    public async Task SendNotification(Notification notification)
    {
        Binding binding = GetBindingForEndpoint(new TimeSpan(0, 0, 30));
        EndpointAddress endpointAddress = GetEndpointAddress(_notificationSettings.ServiceEndpoint);
        NotificationAgencyExternalBasicClient client = new(binding, endpointAddress);

        StandaloneNotificationBEList standaloneNotifications = new()
        {
            new StandaloneNotification
            {
                IsReservable = notification.IsReservable,
                LanguageID = ConvertLanguage(notification.LanguageId),
                ReporteeNumber = notification.ReporteeNumber,
                NotificationType = notification.NotificationType,
                ReceiverEndPoints = new()
                {
                    new ReceiverEndPoint()
                    {
                        TransportType = TransportType.Email,
                        ReceiverAddress = ""
                    }
                },
                TextTokens = new()
                {
                    new TextToken
                    {
                        TokenNum =  0,
                        TokenValue = ""
                    }
                }
            }
        };

        await client.SendStandaloneNotificationBasicV3Async(
            _notificationSettings.Username, 
            _notificationSettings.Password, 
            standaloneNotifications);
    }

    private static int ConvertLanguage(string language)
    {
        switch (language)
        {
            case "en":
                return 1033;
            case "no":
                return 1044;
            case "nn":
                return 2068;
            default:
                return 1044;
        }
    }

    private static Binding GetBindingForEndpoint(TimeSpan timeout)
    {
        var httpsBinding = new BasicHttpBinding();
        httpsBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
        httpsBinding.Security.Mode = BasicHttpSecurityMode.Transport;

        var integerMaxValue = int.MaxValue;
        httpsBinding.MaxReceivedMessageSize = integerMaxValue;
        httpsBinding.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
        httpsBinding.AllowCookies = true;

        httpsBinding.ReceiveTimeout = timeout;
        httpsBinding.SendTimeout = timeout;
        httpsBinding.OpenTimeout = timeout;
        httpsBinding.CloseTimeout = timeout;

        return httpsBinding;
    }

    private static EndpointAddress GetEndpointAddress(string endpointUrl)
    {
        if (!endpointUrl.StartsWith("https://"))
        {
            throw new UriFormatException("The endpoint URL must start with https://.");
        }

        return new EndpointAddress(endpointUrl);
    }
}