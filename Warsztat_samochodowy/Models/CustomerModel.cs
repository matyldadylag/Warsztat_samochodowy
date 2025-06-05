using System.ComponentModel.DataAnnotations;
namespace Warsztat_samochodowy.Models
{
    public class CustomerModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Imię i nazwisko musi mieć nie więcej niż 100 znaków")]
        [MinLength(3, ErrorMessage = "Imię i nazwisko musi mieć przynajmniej 3 znaki")]
        public string FullName { get; set; } = default!;
        [Required]
        [EmailAddress(ErrorMessage ="Niepoprawny adres email")]
        public string Email { get; set; } = default!;
        [Required]
        [Phone(ErrorMessage ="Niepoprawny numer telefonu")]
        public string PhoneNumber { get; set; } = default!;
        public List<VehicleModel> Vehicles { get; set; } = new();
    }
}
