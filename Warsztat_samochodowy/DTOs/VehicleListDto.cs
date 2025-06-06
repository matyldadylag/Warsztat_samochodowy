namespace Warsztat_samochodowy.DTOs
{
    public class VehicleListDto
    {
        public Guid Id { get; set; }
        public string Make { get; set; } = default!;
        public string Model { get; set; } = default!;
        public string LicensePlate { get; set; } = default!;
        public string CustomerName { get; set; } = default!;
    }
}