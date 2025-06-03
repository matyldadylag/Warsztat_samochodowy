namespace Warsztat_samochodowy.Models
{
    public class Comment
    {
        public string Author { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
