namespace MyWebAPI.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int LocationId { get; set; }
    }
}