using System.Xml.Linq;

namespace Warsztat_samochodowy.Models
{
    public class ServiceOrderModel
    {
        public Guid Id { get; set; }
        public ServiceOrderStatus Status { get; set; }
        public string? AssignedMechanic { get; set; }
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
