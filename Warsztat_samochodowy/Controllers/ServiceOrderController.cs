using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Warsztat_samochodowy.Data;
using Warsztat_samochodowy.DTOs;
using Warsztat_samochodowy.Models;

namespace Warsztat_samochodowy.Controllers
{
    public class ServiceOrderController : Controller
    {
        private readonly WorkshopDbContext _context;
        private readonly UserManager<ApplicationUserModel> _userManager;

        public ServiceOrderController(WorkshopDbContext context, UserManager<ApplicationUserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
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
        [HttpGet]
        public async Task<IActionResult> Create(Guid vehicleId)
        {
            var mechanics = await _userManager.GetUsersInRoleAsync("Mechanik");

            var dto = new ServiceOrderCreateDto
            {
                VehicleId = vehicleId,
                AvailableMechanics = mechanics.Select(m => new SelectListItem
                {
                    Value = m.Id,
                    Text = m.Email // lub m.UserName, jak wolisz
                })
            };

            return View(dto);
        }

        // POST: ServiceOrder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceOrderCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                var mechanics = await _userManager.GetUsersInRoleAsync("Mechanik");
                dto.AvailableMechanics = mechanics.Select(m => new SelectListItem
                {
                    Value = m.Id,
                    Text = m.Email
                });

                return View(dto);
            }

            var selectedMechanic = await _userManager.FindByIdAsync(dto.AssignedMechanicId);

            var serviceOrder = new ServiceOrderModel
            {
                Id = Guid.NewGuid(),
                VehicleId = dto.VehicleId,
                Status = dto.Status,
                AssignedMechanic = selectedMechanic?.Email ?? "(nieznany)", // lub .UserName
                CreatedAt = DateTime.UtcNow,
                Comments = new List<CommentModel>(),
                Tasks = new List<ServiceTaskModel>()
            };

            _context.ServiceOrders.Add(serviceOrder);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
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

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var order = await _context.ServiceOrders.FindAsync(id);
            if (order == null)
                return NotFound();

            var dto = new ServiceOrderEditDto
            {
                Id = order.Id,
                AssignedMechanic = order.AssignedMechanic,
                Status = order.Status
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ServiceOrderEditDto dto)
        {
            if (id != dto.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(dto);

            var order = await _context.ServiceOrders.FindAsync(id);
            if (order == null)
                return NotFound();

            order.Status = dto.Status;
            order.AssignedMechanic = dto.AssignedMechanic;

            _context.Update(order);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(CommentCreateDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                // Przy błędzie wróć do Details i pokaż błędy (możesz też przekazać ModelState do widoku)
                return RedirectToAction(nameof(Details), new { id = commentDto.ServiceOrderId });
            }

            var comment = new CommentModel
            {
                Id = Guid.NewGuid(),
                ServiceOrderId = commentDto.ServiceOrderId,
                Author = commentDto.Author,
                Content = commentDto.Content,
                Timestamp = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = commentDto.ServiceOrderId });
        }

    }
}