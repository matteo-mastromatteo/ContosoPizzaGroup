namespace ContosoPizza.Models
{
    public enum EventType {
        Movie,
        Book
    }

    public class Event
    {
        public int Id { get; set; }
        public EventType Type { get; set; }
        public string Title { get; set; }
        public decimal Rating { get; set; }
    }
}