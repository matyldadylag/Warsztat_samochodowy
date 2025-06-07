using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warsztat_samochodowy.Data;
using Warsztat_samochodowy.DTOs;
using Warsztat_samochodowy.Models;

namespace Warsztat_samochodowy.Controllers
{
    public class ServiceOrderController : Controller
    {
        private readonly WorkshopDbContext _context;

        public ServiceOrderController(WorkshopDbContext context)
        {
            _context = context;
        }

        // GET: ServiceOrder
        public async Task<IActionResult> Index(string? searchString)
        {
            var query = _context.ServiceOrders.Include(o => o.Vehicle).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(o =>
                    (o.AssignedMechanic != null && o.AssignedMechanic.Contains(searchString)) ||
                    o.Vehicle.Make.Contains(searchString) ||
                    o.Vehicle.Model.Contains(searchString));
            }

            var serviceOrders = await query
                .Select(o => new ServiceOrderListDto
                {
                    Id = o.Id,
                    Status = o.Status.ToString(),
                    AssignedMechanic = o.AssignedMechanic,
                    VehicleId = o.VehicleId,
                    CreatedAt = o.CreatedAt,
                    VehicleMake = o.Vehicle.Make,
                    VehicleModel = o.Vehicle.Model
                })
                .ToListAsync();

            return View(serviceOrders);
        }

        // GET: ServiceOrder/Create?vehicleId=...
        public IActionResult Create(Guid vehicleId)
        {
            var dto = new ServiceOrderCreateDto
            {
                VehicleId = vehicleId
            };
            return View(dto);
        }

        // POST: ServiceOrder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceOrderCreateDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var serviceOrder = new ServiceOrderModel
            {
                Id = Guid.NewGuid(),
                VehicleId = dto.VehicleId,
                Status = ServiceOrderStatus.New,
                AssignedMechanic = dto.AssignedMechanic,
                CreatedAt = DateTime.UtcNow,
                Comments = new List<CommentModel>(),
                Tasks = new List<ServiceTaskModel>()
            };

            _context.ServiceOrders.Add(serviceOrder);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "ServiceOrder", new { id = dto.VehicleId });
        }

        // GET: ServiceOrder/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var order = await _context.ServiceOrders
                .Include(o => o.Vehicle)
                .Include(o => o.Tasks)
                .Include(o => o.Comments)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        // GET: ServiceOrder/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var order = await _context.ServiceOrders.FindAsync(id);
            if (order == null)
                return NotFound();

            return View(order);
        }

        // POST: ServiceOrder/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ServiceOrderModel updatedOrder)
        {
            if (id != updatedOrder.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(updatedOrder);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceOrderExists(updatedOrder.Id))
                        return NotFound();
                    else
                        throw;
                }
            }
            return View(updatedOrder);
        }

        // GET: ServiceOrder/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var order = await _context.ServiceOrders
                .Include(o => o.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
                return NotFound();

            return View(order);
        }

        // POST: ServiceOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var order = await _context.ServiceOrders.FindAsync(id);
            if (order != null)
            {
                _context.ServiceOrders.Remove(order);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceOrderExists(Guid id)
        {
            return _context.ServiceOrders.Any(e => e.Id == id);
        }
    }
}