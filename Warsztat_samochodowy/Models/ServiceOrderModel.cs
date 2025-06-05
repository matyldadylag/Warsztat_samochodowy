using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
namespace Warsztat_samochodowy.Models
{
    public class ServiceOrderModel
    {
        public Guid Id { get; set; }
        [Required]
        public ServiceOrderStatus Status { get; set; }
        [StringLength(100, ErrorMessage = "Imię mechanika nie może przekraczać 100 znaków")]
        public string? AssignedMechanic { get; set; }
        [Required]
        public Guid VehicleId { get; set; }
        public VehicleModel Vehicle { get; set; } = default!;
        public List<ServiceTaskModel> Tasks { get; set; } = new();
        public List<CommentModel> Comments { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum ServiceOrderStatus
    {
        New,
        InProgress,
        Completed,
        Cancelled
    }
}
