using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warsztat_samochodowy.Data;
using Warsztat_samochodowy.Models;
using Warsztat_samochodowy.DTOs;

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
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return NotFound();

            var commentsDto = order.Comments
                .OrderByDescending(c => c.Timestamp)
                .Select(c => new CommentListDto
                {
                    Id = c.Id,
                    Author = c.Author,
                    Content = c.Content,
                    Timestamp = c.Timestamp
                })
                .ToList();

            ViewData["OrderId"] = order.Id;

            return View(commentsDto);
        }

        public IActionResult Create(Guid orderId)
        {
            var dto = new CommentCreateDto
            {
                ServiceOrderId = orderId
            };
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommentCreateDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var comment = new CommentModel
            {
                Id = Guid.NewGuid(),
                Author = dto.Author,
                Content = dto.Content,
                Timestamp = DateTime.UtcNow,
                ServiceOrderId = dto.ServiceOrderId
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { orderId = dto.ServiceOrderId });
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

            return RedirectToAction("Index", new { orderId });
        }
    }
}