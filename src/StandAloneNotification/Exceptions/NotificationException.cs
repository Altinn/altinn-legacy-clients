namespace StandAloneNotification.Exceptions
{
    public class NotificationException : Exception
    {
        public string? AltinnErrorMessage;

        public string? AltinnExtendedErrorMessage;

        public string? AltinnLocalizedErrorMessage;

        public string? ErrorGuid;

        public int ErrorID;

        public string? UserGuid;

        public string? UserId;

        public NotificationException() { }
        public NotificationException(string message) : base(message) { }
        public NotificationException(string message, Exception inner) : base(message, inner) { }

    }
}
