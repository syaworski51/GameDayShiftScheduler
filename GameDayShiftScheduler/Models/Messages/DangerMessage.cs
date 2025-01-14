namespace GameDayShiftScheduler.Models.Messages
{
    public class DangerMessage : Message
    {
        public DangerMessage(string body) : base("danger", body) { }
    }
}
