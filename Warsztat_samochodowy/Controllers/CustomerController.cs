using Microsoft.AspNetCore.Mvc;
using Warsztat_samochodowy.Models;
using Warsztat_samochodowy.Data;
using Warsztat_samochodowy.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Warsztat_samochodowy.Controllers
{
    public class CustomerController : Controller
    {
        private readonly WorkshopDbContext _context;
        public CustomerController(WorkshopDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString)
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                var searchParts = searchString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                query = query.Where(c =>
                    searchParts.All(part =>
                        c.FirstName.Contains(part) ||
                        c.LastName.Contains(part))
                    || c.Email.Contains(searchString)
                    || c.PhoneNumber.Contains(searchString));
            }

            var customers = query
                .Select(c => new CustomerListDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber
                })
                .ToList();

            return View(customers);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomerCreateDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var customer = new CustomerModel
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound();

            var dto = new CustomerEditDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CustomerEditDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var customer = _context.Customers.Find(dto.Id);
            if (customer == null) return NotFound();

            customer.FirstName = dto.FirstName;
            customer.LastName = dto.LastName;
            customer.Email = dto.Email;
            customer.PhoneNumber = dto.PhoneNumber;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound();

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound();

            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        // GET: Customer/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var customer = _context.Customers
                .Include(c => c.Vehicles)  // ładowanie pojazdów klienta
                .FirstOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }
    }
}