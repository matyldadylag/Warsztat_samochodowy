using System.Xml.Linq;

namespace Warsztat_samochodowy.Models
{
    public class ServiceOrder
    {
        public ServiceOrderStatus Status { get; set; }
        public string? AssignedMechanic { get; set; }
        public List<ServiceTask> Tasks { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
    }

    public enum ServiceOrderStatus
    {
        New,
        InProgress,
        Completed,
        Cancelled
    }
}
