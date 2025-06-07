using System.ComponentModel.DataAnnotations;
using Warsztat_samochodowy.Models;

namespace Warsztat_samochodowy.DTOs
{
    public class ServiceOrderCreateDto
    {
        [Required]
        public Guid VehicleId { get; set; }

        [StringLength(100, ErrorMessage = "Imię mechanika nie może przekraczać 100 znaków")]
        public string? AssignedMechanic { get; set; }

        [Required]
        public ServiceOrderStatus Status { get; set; } = ServiceOrderStatus.New;
    }
}
