using Microsoft.AspNetCore.Mvc;

namespace Warsztat_samochodowy.DTOs
{
    public class ServiceTaskListDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal LaborCost { get; set; }
    }

}
