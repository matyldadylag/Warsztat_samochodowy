namespace Warsztat_samochodowy.Models
{
    public class VehicleModel
    {
        public Guid Id { get; set; }
        public string Make { get; set; } = default!;
        public string Model { get; set; } = default!;
        public string LicensePlate { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public Guid CustomerId { get; set; }
        public CustomerModel Customer { get; set; } = default!;
    }
}
