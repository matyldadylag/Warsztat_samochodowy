using Microsoft.AspNetCore.Mvc;
using Warsztat_samochodowy.Models;
using Warsztat_samochodowy.Data;
using Warsztat_samochodowy.DTOs;

namespace Warsztat_samochodowy.Controllers
{
    public class CustomerController : Controller
    {
        private readonly WorkshopDbContext _context;
        public CustomerController(WorkshopDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var customers = _context.Customers
                .Select(c => new CustomerListDto
                {
                    Id = c.Id,
                    FullName = c.FullName
                })
                .ToList();

            return View(customers);
        }
        // GET: Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomerCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var customer = new CustomerModel
            {
                Id = Guid.NewGuid(),
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Vehicles = new List<VehicleModel>()
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
