namespace Warsztat_samochodowy.DTOs
{
    public class CustomerCreateDto
    {
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }
}