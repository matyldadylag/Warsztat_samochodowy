namespace Warsztat_samochodowy.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public List<Vehicle> Vehicles { get; set; } = new();
    }
}
