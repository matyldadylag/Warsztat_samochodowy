using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warsztat_samochodowy.Data;
using Warsztat_samochodowy.Models;

namespace Warsztat_samochodowy.Controllers
{
    public class CommentController : Controller
    {
        private readonly WorkshopDbContext _context;

        public CommentController(WorkshopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(Guid orderId)
        {
            var order = await _context.ServiceOrders
                .Include(o => o.Comments)
                .Include(o => o.Vehicle)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return NotFound();

            ViewBag.OrderId = order.Id;
            ViewBag.OrderTitle = $"{order.Vehicle.Make} {order.Vehicle.Model}";

            return View(order.Comments.OrderByDescending(c => c.Timestamp).ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommentModel comment)
        {
            // Wyłącz walidację powiązanego obiektu – bo i tak go przypiszemy ręcznie
            ModelState.Remove("ServiceOrder");

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Any())
                    .Select(x => $"{x.Key}: {string.Join(", ", x.Value.Errors.Select(e => e.ErrorMessage))}");

                var errorMessage = "ModelState is invalid:\n" + string.Join("\n", errors);
                return Content(errorMessage);
            }

            var order = await _context.ServiceOrders.FindAsync(comment.ServiceOrderId);
            if (order == null)
                return NotFound();

            comment.Id = Guid.NewGuid();
            comment.Timestamp = DateTime.UtcNow;
            comment.ServiceOrder = order;

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "ServiceOrder", new { id = comment.ServiceOrderId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
                return NotFound();

            var orderId = comment.ServiceOrderId;

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { orderId = orderId });
        }
    }
}
