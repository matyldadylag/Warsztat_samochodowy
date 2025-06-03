namespace Warsztat_samochodowy.Models
{
    public class CustomerModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public List<VehicleModel> Vehicles { get; set; } = new();
    }
}
