using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Warsztat_samochodowy.Models;

namespace Warsztat_samochodowy.DTOs
{
    public class ServiceOrderCreateDto
    {
        [Required]
        public Guid VehicleId { get; set; }
        [Required(ErrorMessage = "Wybierz mechanika")]
        public string AssignedMechanicId { get; set; }

        [Required]
        public ServiceOrderStatus Status { get; set; } = ServiceOrderStatus.New;

        public IEnumerable<SelectListItem>? AvailableMechanics { get; set; }
    }
}
