namespace GameDayShiftScheduler.Models.Messages
{
    /// <summary>
    ///     Represents a message that will be broadcast in a Bootstrap-style message bubble.
    /// </summary>
    public class Message
    {
        /// <summary>
        ///     The type of message. Corresponds to a Bootstrap class (info, success, warning, danger, etc.).
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     The main contents of the message.
        /// </summary>
        public string Body { get; set; }


        public Message(string type, string body)
        {
            Type = type;
            Body = body;
        }
    }
}
