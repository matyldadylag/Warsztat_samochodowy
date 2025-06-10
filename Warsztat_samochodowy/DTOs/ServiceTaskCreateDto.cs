using System.ComponentModel.DataAnnotations;
using Warsztat_samochodowy.DTOs;

namespace Warsztat_samochodowy.Models
{
    public class ServiceTaskCreateDto
    {
        [Required]
        public Guid ServiceOrderId { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "Opis musi mieć od 5 do 300 znaków")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0, 1000000, ErrorMessage = "Koszt musi być większy lub równy 0")]
        public decimal LaborCost { get; set; }
        public List<UsedPartCreateDto> UsedParts { get; set; } = new();
    }
}
