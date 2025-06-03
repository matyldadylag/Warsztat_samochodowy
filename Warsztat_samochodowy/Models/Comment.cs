namespace Warsztat_samochodowy.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Author { get; set; } = default!;
        public string Content { get; set; } = default!;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public Guid ServiceOrderId { get; set; }
        public ServiceOrder ServiceOrder { get; set; } = default!;
    }
}
