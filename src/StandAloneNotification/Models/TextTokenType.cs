namespace StandAloneNotification.Models
{
    public class TextTokenType
    {
        /// <summary>
        /// Number of token to be substitued
        /// </summary>
        public int TokenNumber { get; set; }

        /// <summary>
        /// Value to replace with token
        /// </summary>
        public string TokenValue { get; set; } = string.Empty;
    }
}
