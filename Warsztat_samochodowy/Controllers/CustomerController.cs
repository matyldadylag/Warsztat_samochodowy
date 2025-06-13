using Microsoft.AspNetCore.Mvc;
using Warsztat_samochodowy.Models;
using Warsztat_samochodowy.Data;
using Warsztat_samochodowy.DTOs;
using Warsztat_samochodowy.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Warsztat_samochodowy.Controllers
{
    public class CustomerController : Controller
    {
        private readonly WorkshopDbContext _context;
        private readonly CustomerMapper _mapper;

        public CustomerController(WorkshopDbContext context, CustomerMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

            var customers = query.ToList()
                .Select(c => _mapper.ToListDto(c))
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

            var customer = _mapper.ToModel(dto);
            customer.Id = Guid.NewGuid();

            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
                return NotFound();

            var dto = _mapper.ToEditDto(customer);

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CustomerEditDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var customer = _context.Customers.Find(dto.Id);
            if (customer == null)
                return NotFound();

            var updatedCustomer = _mapper.ToModel(dto);

            customer.FirstName = updatedCustomer.FirstName;
            customer.LastName = updatedCustomer.LastName;
            customer.Email = updatedCustomer.Email;
            customer.PhoneNumber = updatedCustomer.PhoneNumber;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
                return NotFound();

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var customer = _context.Customers
                .Include(c => c.Vehicles)
                .FirstOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }
    }
}