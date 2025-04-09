namespace MyWebAPI.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        public int LocationId { get; set; }
        public Location? Location { get; set; }

        public ICollection<Session>? Sessions { get; set; }
        public ICollection<EventParticipant>? EventParticipants { get; set; }
    }
}