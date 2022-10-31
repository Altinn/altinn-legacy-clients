namespace StandAloneNotification.Models
{
    public class ReceiverEndPointType
    {
        /// <summary>
        /// Address to receiverEndpoint. Email or phonenumber (for sms)
        /// </summary>
        public string ReceiverAddress { get; set; } = string.Empty;

        /// <summary>
        /// Transporttype for notification, see #ReceiverTransportType
        /// </summary>
        public ReceiverTransportType ReceiverTransportType { get; set; }

        public ReceiverEndPointType(string receiverAdress, ReceiverTransportType receiverTransportType)
        {
            ReceiverAddress = receiverAdress;
            ReceiverTransportType = receiverTransportType;
        }
    }

    /// <summary>
    /// Different valid transporttypes for notification
    /// </summary>
    public enum ReceiverTransportType : int
    {
        SMS = 1,
        Email = 2,
        IM = 3,
        Both = 4,
        SMSPreferred = 5,
        EmailPreferred = 6,
    }
}
