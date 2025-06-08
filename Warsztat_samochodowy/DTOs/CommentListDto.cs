using Microsoft.AspNetCore.Mvc;

namespace Warsztat_samochodowy.DTOs
{
    public class CommentListDto
    {
        public Guid Id { get; set; }
        public string Author { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }

}
