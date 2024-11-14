namespace GameDayShiftScheduler.Models
{
    /// <summary>
    ///     Represents a message that will be broadcast in a Bootstrap-style message bubble.
    /// </summary>
    public class Message
    {
        /// <summary>
        ///     The class that will be used to convey the nature of the message (info, success, warning, danger, etc.).
        /// </summary>
        public string BootstrapClass { get; set; }

        /// <summary>
        ///     The main contents of the message.
        /// </summary>
        public string Body { get; set; }
    }
}
