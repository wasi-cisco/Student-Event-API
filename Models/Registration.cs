namespace StudentEvents.Api.Models
{
    public class Registration
    {
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public int EventId { get; set; }
        public Event Event { get; set; } = null!;

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }
}
