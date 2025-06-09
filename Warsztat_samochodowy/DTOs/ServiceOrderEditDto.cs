using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Warsztat_samochodowy.Models;

namespace Warsztat_samochodowy.DTOs
{
    public class ServiceOrderEditDto
    {
        public Guid Id { get; set; }

        [Required]
        public string AssignedMechanic { get; set; } = default!;

        [Required]
        public ServiceOrderStatus Status { get; set; }
        public IEnumerable<SelectListItem>? AvailableMechanics { get; set; }
    }
}