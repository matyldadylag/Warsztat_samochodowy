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
                .Include(t => t.UsedParts)
                    .ThenInclude(up => up.Part)
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

            ViewBag.Parts = _context.Parts.ToList();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceTaskCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Parts = _context.Parts.ToList();
                return View(dto);
            }

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

            foreach (var partDto in dto.UsedParts)
            {
                task.UsedParts.Add(new UsedPartModel
                {
                    Id = Guid.NewGuid(),
                    PartId = partDto.PartId,
                    Quantity = partDto.Quantity,
                    ServiceTaskId = task.Id
                });
            }

            _context.ServiceTasks.Add(task);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { orderId = dto.ServiceOrderId });
        }
    }
}