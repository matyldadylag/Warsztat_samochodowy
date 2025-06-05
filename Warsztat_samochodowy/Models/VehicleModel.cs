using System.ComponentModel.DataAnnotations;

namespace Warsztat_samochodowy.Models
{
    public class VehicleModel
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Marka musi mieć od 1 do 50 znaków")]
        public string Make { get; set; } = default!;
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Model musi mieć od 1 do 50 znaków")]
        public string Model { get; set; } = default!;
        [Required]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Numer rejestracyjny musi mieć od 5 do 10 znaków")]
        public string LicensePlate { get; set; } = default!;
        [StringLength(255, ErrorMessage = "Adres URL może mieć maksymalnie 255 znaków")]
        public string ImageUrl { get; set; } = default!;
        [Required]
        public Guid CustomerId { get; set; }
        public CustomerModel Customer { get; set; } = default!;
    }
}
