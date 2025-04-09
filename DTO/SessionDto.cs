namespace MyWebAPI.Dtos
{
    public class SessionDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EventId { get; set; }
        public int RoomId { get; set; }
    }
}