using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Warsztat_samochodowy.DTOs
{
    public class VehicleCreateDto
    {
        [Required(ErrorMessage = "Marka jest wymagana")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Marka musi mieć od 1 do 50 znaków")]
        public string Make { get; set; } = default!;

        [Required(ErrorMessage = "Model jest wymagany")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Model musi mieć od 1 do 50 znaków")]
        public string Model { get; set; } = default!;

        [Required(ErrorMessage = "Numer rejestracyjny jest wymagany")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Numer rejestracyjny musi mieć od 5 do 10 znaków")]
        public string LicensePlate { get; set; } = default!;

        [Required(ErrorMessage = "Numer VIN jest wymagany")]
        [StringLength(17, ErrorMessage = "VIN musi mieć 17 znaków")]
        public string VIN { get; set; } = default!;

        [StringLength(255, ErrorMessage = "Adres URL może mieć maksymalnie 255 znaków")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pole wymagane")]
        public Guid CustomerId { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}