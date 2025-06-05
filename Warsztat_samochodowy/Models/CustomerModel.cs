using System.ComponentModel.DataAnnotations;
namespace Warsztat_samochodowy.Models
{
    public class CustomerModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Imi ęmusi mieć nie więcej niż 100 znaków")]
        [MinLength(3, ErrorMessage = "Imię musi mieć przynajmniej 3 znaki")]
        public string FirstName { get; set; } = default!;
        [Required]
        [MaxLength(100, ErrorMessage = "Nazwisko musi mieć nie więcej niż 100 znaków")]
        [MinLength(3, ErrorMessage = "Nazwisko musi mieć przynajmniej 3 znaki")]
        public string LastName { get; set; } = default!;
        [Required]
        [EmailAddress(ErrorMessage ="Niepoprawny adres email")]
        public string Email { get; set; } = default!;
        [Required]
        [Phone(ErrorMessage ="Niepoprawny numer telefonu")]
        public string PhoneNumber { get; set; } = default!;
        public List<VehicleModel> Vehicles { get; set; } = new();
    }
}
