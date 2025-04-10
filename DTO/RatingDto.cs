namespace MyWebAPI.Dtos
{
    public class RatingDto
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int ParticipantId { get; set; }
        public int Score { get; set; }
        public string? Comment { get; set; }
    }
}