using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warsztat_samochodowy.Data;
using Warsztat_samochodowy.Models;

namespace Warsztat_samochodowy.Controllers
{
    public class PartController : Controller
    {
        private readonly WorkshopDbContext _context;

        public PartController(WorkshopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var parts = await _context.Parts.ToListAsync();
            return View(parts);
        }

        public IActionResult Create()
        {
            return View(new PartModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartModel part)
        {
            if (!ModelState.IsValid)
                return View(part);

            part.Id = Guid.NewGuid();
            _context.Parts.Add(part);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var part = await _context.Parts.FindAsync(id);
            if (part == null)
                return NotFound();

            return View(part);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PartModel part)
        {
            if (!ModelState.IsValid)
                return View(part);

            _context.Parts.Update(part);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var part = await _context.Parts.FindAsync(id);
            if (part == null)
                return NotFound();

            return View(part);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var part = await _context.Parts.FindAsync(id);
            if (part != null)
            {
                _context.Parts.Remove(part);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}