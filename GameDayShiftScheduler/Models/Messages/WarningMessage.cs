namespace GameDayShiftScheduler.Models.Messages
{
    public class WarningMessage : Message
    {
        public WarningMessage(string body) : base("warning", body) { }
    }
}
