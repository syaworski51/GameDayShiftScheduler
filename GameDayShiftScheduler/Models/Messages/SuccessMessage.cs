namespace GameDayShiftScheduler.Models.Messages
{
    public class SuccessMessage : Message
    {
        public SuccessMessage(string body) : base("success", body) { }
    }
}
