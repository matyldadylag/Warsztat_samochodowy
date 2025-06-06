using System.ComponentModel.DataAnnotations;

namespace Warsztat_samochodowy.DTOs
{
    public class CustomerCreateDto
    {
        [Required(ErrorMessage = "Pole wymagane")]
        [MaxLength(100, ErrorMessage = "Imię musi mieć nie więcej niż 100 znaków")]
        [MinLength(3, ErrorMessage = "Imię musi mieć przynajmniej 3 znaki")]
        public string FirstName { get; set; } = default!;

        [Required(ErrorMessage = "Pole wymagane")]
        [MaxLength(100, ErrorMessage = "Nazwisko musi mieć nie więcej niż 100 znaków")]
        [MinLength(3, ErrorMessage = "Nazwisko musi mieć przynajmniej 3 znaki")]
        public string LastName { get; set; } = default!;

        [Required(ErrorMessage = "Pole wymagane")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres email")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Pole wymagane")]
        [Phone(ErrorMessage = "Niepoprawny numer telefonu")]
        public string PhoneNumber { get; set; } = default!;
    }

}