using System.ComponentModel.DataAnnotations;

namespace Warsztat_samochodowy.Models
{
    public class UsedPartModel
    {
        public Guid Id { get; set; }
        [Required]
        public Guid PartId { get; set; }
        public PartModel Part { get; set; } = default!;
        [Range(1, int.MaxValue, ErrorMessage = "Ilość części musi być co najmniej 1")]
        public int Quantity { get; set; }
        [Required]
        public Guid ServiceTaskId { get; set; }
        public ServiceTaskModel ServiceTask { get; set; } = default!;
    }
}
