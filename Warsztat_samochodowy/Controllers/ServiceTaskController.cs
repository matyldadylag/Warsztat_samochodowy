using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warsztat_samochodowy.Data;
using Warsztat_samochodowy.Models;

namespace Warsztat_samochodowy.Controllers
{
    public class ServiceTaskController : Controller
    {
        private readonly WorkshopDbContext _context;

        public ServiceTaskController(WorkshopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(Guid orderId)
        {
            var tasks = await _context.ServiceTasks
                .Where(t => t.ServiceOrderId == orderId)
                .OrderByDescending(t => t.Description)
                .ToListAsync();

            return View((orderId, tasks));
        }

        [HttpGet]
        public IActionResult Create(Guid orderId)
        {
            var dto = new ServiceTaskCreateDto
            {
                ServiceOrderId = orderId
            };
            return View(dto);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceTaskCreateDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            // Znajdź zlecenie po ID, które przyszło z formularza
            var serviceOrder = await _context.ServiceOrders.FindAsync(dto.ServiceOrderId);
            if (serviceOrder == null)
                return NotFound("Zlecenie nie istnieje.");

            var task = new ServiceTaskModel
            {
                Id = Guid.NewGuid(),
                Description = dto.Description,
                LaborCost = dto.LaborCost,
                ServiceOrderId = dto.ServiceOrderId,
                ServiceOrder = serviceOrder 
            };

            _context.ServiceTasks.Add(task);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { orderId = dto.ServiceOrderId });
        }

    }
}
