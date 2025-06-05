using System.ComponentModel.DataAnnotations;
namespace Warsztat_samochodowy.Models
{
    public class CommentModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Author { get; set; } = default!;
        [Required]
        [MaxLength(1000, ErrorMessage = "Twój komentarz nie może przekraczać 1000 znaków")]
        public string Content { get; set; } = default!;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        [Required]
        public Guid ServiceOrderId { get; set; }
        [Required]
        public ServiceOrderModel ServiceOrder { get; set; } = default!;
    }
}
