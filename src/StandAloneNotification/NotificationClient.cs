using System.ServiceModel;
using System.ServiceModel.Channels;

using AltinnII.Services.Notification;
using Microsoft.Extensions.Options;
using StandAloneNotification.Models;

namespace StandAloneNotification;

public class NotificationClient : INotificationClient
{
    private readonly NotificationSettings _notificationSettings;

    public NotificationClient(IOptions<NotificationSettings> notificationSettings)
    {
        _notificationSettings = notificationSettings.Value;
    }

    public async Task SendNotification(List<Notification> notificationList)
    {
        Binding binding = GetBindingForEndpoint(new TimeSpan(0, 0, 30));
        EndpointAddress endpointAddress = GetEndpointAddress(_notificationSettings.ServiceEndpoint);
        NotificationAgencyExternalBasicClient client = new(binding, endpointAddress);

        SendStandaloneNotificationBasicV3Response response = await client.SendStandaloneNotificationBasicV3Async(
                _notificationSettings.Username,
                _notificationSettings.Password,
                GetStandaloneNotifications(notificationList));
    }

    private static StandaloneNotificationBEList GetStandaloneNotifications(List<Notification> notificationList)
    {
        StandaloneNotificationBEList standaloneNotifications = new();
        foreach (Notification notification in notificationList)
        {
            standaloneNotifications.Add(new StandaloneNotification
            {
                IsReservable = notification.IsReservable,
                LanguageID = ConvertLanguage(notification.LanguageId),
                ReporteeNumber = notification.ReporteeNumber,
                NotificationType = notification.NotificationType,
                Service = GetService(notification),
                Roles = GetRoles(notification),
                ReceiverEndPoints = GetReceiverEndpoints(notification),
                TextTokens = GetTextTokens(notification)
            });
        }
        return standaloneNotifications;
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

    private static Service? GetService(Notification notification)
    {
        if (!string.IsNullOrEmpty(notification.ServiceCode) && notification.ServiceEdition > 0)
        {
            return new()
            {
                ServiceCode = notification.ServiceCode,
                ServiceEdition = notification.ServiceEdition
            };
        }
        return null;
    }

    private static Roles? GetRoles(Notification notification)
    {
        if (notification.Roles != null && notification.Roles.Count > 0)
        {
            Roles roles = new();
            foreach(string element in notification.Roles)
            {
                roles.Add(element);
            }
            return roles;
        }
        return null;
    }

    private static ReceiverEndPointBEList? GetReceiverEndpoints(Notification notification)
    {
        if(notification.ReceiverEndPoints != null && notification.ReceiverEndPoints.Count > 0)
        {
            ReceiverEndPointBEList receiverEndpoints = new ReceiverEndPointBEList();
            foreach(ReceiverEndPointType element in notification.ReceiverEndPoints)
            {
                receiverEndpoints.Add(new ReceiverEndPoint()
                {
                    TransportType = (TransportType) element.ReceiverTransportType,
                    ReceiverAddress = element.ReceiverAddress
                });
            }
            return receiverEndpoints;
        }
        return null;
    }

    private static TextTokenSubstitutionBEList? GetTextTokens(Notification notification)
    {
        if (notification.TextTokens != null && notification.TextTokens.Count > 0)
        { 
            TextTokenSubstitutionBEList textTokens = new TextTokenSubstitutionBEList();
            foreach (TextTokenType element in notification.TextTokens)
            {
                textTokens.Add(new TextToken
                {
                    TokenNum = element.TokenNumber,
                    TokenValue = element.TokenValue
                });
            }
            return textTokens;
        }
        return null;
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