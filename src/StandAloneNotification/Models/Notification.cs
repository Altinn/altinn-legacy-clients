namespace StandAloneNotification.Models;

/// <summary>
/// Defines the necessary details needed to create a notification and to send that through the 
/// Altinn II Notification service.
/// </summary>
public class Notification
{
    /// <summary>
    /// This value tells the notification service which notification type to use. Notification type
    /// is a concept around text templates. This service assumes prior knowledge about what notification
    /// types that are available in Altinn II.
    /// </summary>
    public string NotificationType { get; set; } = string.Empty;

    /// <summary>
    /// This value indicate whether the reportee (person) reservation flag is to be respected.
    /// </summary>
    public bool IsReservable { get; set; }

    /// <summary>
    /// This value tells the notification service which language to use when selecting texts from the
    /// given notification type. Default language is no - Norwegian bokmål.
    /// </summary>
    public string LanguageId { get; set; } = "no";

    /// <summary>
    /// The person or organisation that the notification is targeting for identification of recipients and 
    /// authorization.
    /// </summary>
    public string ReporteeNumber { get; set; } = string.Empty;

    /// <summary>
    /// Endpoints for notification. When Reportee is an Organization and ReceiverEndpoint has no ReceiverAddress, 
    /// receiverAddresses will be generated from the Organization Profile, 
    /// and additional ReceiverEndPoints will be generated based on the UnitReportee profile and supplied ServiceCode.
    /// </summary>
    public List<ReceiverEndPointType> ReceiverEndPoints { get; set; } = new();

    /// <summary>
    /// ServiceCode for the service the notification will be authorized against
    /// </summary>
    public string ServiceCode { get; set; } = string.Empty;

    /// <summary>
    /// ServiceEdition for the service the notification will be authorized against
    /// </summary>
    public int ServiceEdition { get; set; }

    /// <summary>
    /// List of roles needed to get this notification
    /// </summary>
    public List<string> Roles { get; set; } = new();

    /// <summary>
    /// List of textTokens to substitute in the notification
    /// </summary>
    public List<TextTokenType> TextTokens { get; set; } = new();
}