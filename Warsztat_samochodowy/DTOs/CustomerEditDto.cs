using System.ComponentModel.DataAnnotations;

namespace Warsztat_samochodowy.DTOs
{
    public class CustomerEditDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane")]
        [MaxLength(100, ErrorMessage = "Imię musi mieć nie więcej niż 100 znaków")]
        [MinLength(3, ErrorMessage = "Imię musi mieć przynajmniej 3 znaki")]
        public string FirstName { get; set; } = default!;

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [MaxLength(100, ErrorMessage = "Nazwisko musi mieć nie więcej niż 100 znaków")]
        [MinLength(3, ErrorMessage = "Nazwisko musi mieć przynajmniej 3 znaki")]
        public string LastName { get; set; } = default!;

        [Required(ErrorMessage = "Adres e-mail jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres email")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        [Phone(ErrorMessage = "Niepoprawny numer telefonu")]
        public string PhoneNumber { get; set; } = default!;
    }
}
