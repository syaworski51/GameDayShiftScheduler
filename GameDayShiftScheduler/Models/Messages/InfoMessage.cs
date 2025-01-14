
namespace GameDayShiftScheduler.Models.Messages
{
    public class InfoMessage : Message
    {
        public InfoMessage(string body) : base("info", body) { }
    }
}
