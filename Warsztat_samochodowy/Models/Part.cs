namespace Warsztat_samochodowy.Models
{
    public class Part
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal UnitPrice { get; set; }
    }
}
