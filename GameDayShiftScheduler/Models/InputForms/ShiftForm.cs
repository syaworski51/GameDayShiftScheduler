namespace GameDayShiftScheduler.Models.InputForms
{
    public class ShiftForm
    {
        public Guid SportId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int TeamMembersRequired { get; set; }
        public List<TeamMember> SelectedTeamMembers { get; set; }
    }
}
