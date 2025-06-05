using System.ComponentModel.DataAnnotations;
namespace Warsztat_samochodowy.Models
{
    public class PartModel
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Nazwa części musi mieć od 2 do 100 znaków")]
        public string Name { get; set; } = default!;
        [Required]
        [Range(0.01, 100000, ErrorMessage = "Cena jest wymagana i nie może przekraczać 100000")]
        public decimal UnitPrice { get; set; }
    }
}
