using System.ComponentModel.DataAnnotations;
namespace Warsztat_samochodowy.Models
{
    public class ServiceTaskModel
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "Opis musi mieć od 5 do 300 znaków")]
        public string Description { get; set; } = default!;
        [Required]
        [Range(0, 1000000, ErrorMessage = "Koszt musi być większy lub równy 0")]
        public decimal LaborCost { get; set; }
        public List<UsedPartModel> UsedParts { get; set; } = new();
        [Required(ErrorMessage = "Zadanie musi być przypisane do zlecenia")]
        public Guid ServiceOrderId { get; set; }
        public ServiceOrderModel ServiceOrder { get; set; } = default!;
    }
}
