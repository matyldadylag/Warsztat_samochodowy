namespace Warsztat_samochodowy.Models
{
    public class ServiceTask
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = default!;
        public decimal LaborCost { get; set; }
        public List<UsedPart> UsedParts { get; set; } = new();
        public Guid ServiceOrderId { get; set; }
        public ServiceOrder ServiceOrder { get; set; } = default!;
    }
}
