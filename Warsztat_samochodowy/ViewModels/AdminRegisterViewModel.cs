using System.ComponentModel.DataAnnotations;

namespace Warsztat_samochodowy.ViewModels
{
    public class AdminRegisterViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Role { get; set; }
    }

}