namespace StandAloneNotification;

/// <summary>
/// This class is a strongly typed set of settings used by the notification client to
/// communicate with the notification service in Altinn II.
/// </summary>
public class NotificationSettings
{
    /// <summary>
    /// The endpoint address of the Altinn II service.
    /// </summary>
    public string ServiceEndpoint { get; set; } = string.Empty;

    /// <summary>
    /// The username of the agency system to be used in authentication against the Altinn II service.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// The username of the agency system to be used in authentication against the Altinn II service.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
