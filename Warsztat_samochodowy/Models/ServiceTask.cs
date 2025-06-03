namespace Warsztat_samochodowy.Models
{
    public class ServiceTask
    {
        public string Description { get; set; } = default!;
        public decimal LaborCost { get; set; }
        public List<UsedPart> UsedParts { get; set; } = new();
    }
}
