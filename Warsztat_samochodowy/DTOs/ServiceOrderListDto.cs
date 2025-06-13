namespace Warsztat_samochodowy.DTOs
{
    public class ServiceOrderListDto
    {
        public Guid Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? AssignedMechanic { get; set; }
        public Guid VehicleId { get; set; }

        public string VehicleMake { get; set; } = string.Empty;
        public string VehicleModel { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}