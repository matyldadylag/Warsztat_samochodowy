namespace Warsztat_samochodowy.Models
{
    public class UsedPart
    {
        public Guid Id { get; set; }
        public Guid PartId { get; set; }
        public Part Part { get; set; } = default!;
        public int Quantity { get; set; }
        public Guid ServiceTaskId { get; set; }
        public ServiceTask ServiceTask { get; set; } = default!;
    }
}
