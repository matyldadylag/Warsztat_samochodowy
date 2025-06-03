namespace Warsztat_samochodowy.Models
{
    public class UsedPartModel
    {
        public Guid Id { get; set; }
        public Guid PartId { get; set; }
        public PartModel Part { get; set; } = default!;
        public int Quantity { get; set; }
        public Guid ServiceTaskId { get; set; }
        public ServiceTaskModel ServiceTask { get; set; } = default!;
    }
}
