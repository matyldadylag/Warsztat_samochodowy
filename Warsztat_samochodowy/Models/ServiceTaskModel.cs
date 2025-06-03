namespace Warsztat_samochodowy.Models
{
    public class ServiceTaskModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = default!;
        public decimal LaborCost { get; set; }
        public List<UsedPartModel> UsedParts { get; set; } = new();
        public Guid ServiceOrderId { get; set; }
        public ServiceOrderModel ServiceOrder { get; set; } = default!;
    }
}
