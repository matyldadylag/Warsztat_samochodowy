using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warsztat_samochodowy.Data;
using Warsztat_samochodowy.DTOs;
using Warsztat_samochodowy.Models;

namespace Warsztat_samochodowy.Controllers
{
    public class VehicleController : Controller
    {
        private readonly WorkshopDbContext _context;

        public VehicleController(WorkshopDbContext context)
        {
            _context = context;
        }

        // GET: Vehicle
        public IActionResult Index(string searchString)
        {
            var query = _context.Vehicles
                .Include(v => v.Customer)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                // Rozbijamy searchString na słowa (np. "SeatAlhambra" -> ["SeatAlhambra"], albo "Seat Alhambra" -> ["Seat", "Alhambra"])
                var searchParts = searchString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                query = query.Where(v =>
                    searchParts.All(part =>
                        v.Make.Contains(part) ||
                        v.Model.Contains(part) ||
                        v.LicensePlate.Contains(part) ||
                        (v.Customer.FirstName + " " + v.Customer.LastName).Contains(part)
                    )
                );
            }

            var vehicles = query
                .Select(v => new VehicleListDto
                {
                    Id = v.Id,
                    Make = v.Make,
                    Model = v.Model,
                    LicensePlate = v.LicensePlate,
                    CustomerName = v.Customer.FirstName + " " + v.Customer.LastName,
                    VIN = v.VIN
                })
                .ToList();

            return View(vehicles);
        }

        // GET: Vehicle/Create?customerId=...
        public IActionResult Create(Guid customerId)
        {
            return View(new VehicleCreateDto { CustomerId = customerId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VehicleCreateDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            string? fileName = null;
            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                // wygeneruj unikalną nazwę pliku
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageFile.FileName);

                // ścieżka do wwwroot/uploads
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                // zapisz plik na dysku
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    dto.ImageFile.CopyTo(stream);
                }
            }

            var vehicle = new VehicleModel
            {
                Id = Guid.NewGuid(),
                Make = dto.Make,
                Model = dto.Model,
                LicensePlate = dto.LicensePlate,
                ImageUrl = fileName != null ? "/uploads/" + fileName : dto.ImageUrl,
                CustomerId = dto.CustomerId,
                VIN = dto.VIN
            };

            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
            return RedirectToAction("Index", "Vehicle", new { id = dto.CustomerId });
        }

        // GET: Vehicle/Edit/{id}
        public IActionResult Edit(Guid id)
        {
            var vehicle = _context.Vehicles.Find(id);
            if (vehicle == null)
                return NotFound();

            var dto = new VehicleEditDto
            {
                Id = vehicle.Id,
                Make = vehicle.Make,
                Model = vehicle.Model,
                LicensePlate = vehicle.LicensePlate,
                ImageUrl = vehicle.ImageUrl,
                CustomerId = vehicle.CustomerId,
                VIN = vehicle.VIN
            };
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(VehicleEditDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            string? fileName = null;
            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                // wygeneruj unikalną nazwę pliku
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageFile.FileName);

                // ścieżka do wwwroot/uploads
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                // zapisz plik na dysku
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    dto.ImageFile.CopyTo(stream);
                }
            }

            var vehicle = _context.Vehicles.Find(dto.Id);
            if (vehicle == null)
                return NotFound();

            vehicle.Make = dto.Make;
            vehicle.Model = dto.Model;
            vehicle.LicensePlate = dto.LicensePlate;
            vehicle.ImageUrl = fileName != null ? "/uploads/" + fileName : dto.ImageUrl;
            vehicle.VIN = dto.VIN;

            _context.SaveChanges();

            return RedirectToAction("Index", "Vehicle", new { id = dto.CustomerId });
        }

        // GET: Vehicle/Delete/{id}
        public IActionResult Delete(Guid id)
        {
            var vehicle = _context.Vehicles
                .Include(v => v.Customer)
                .FirstOrDefault(v => v.Id == id);

            if (vehicle == null)
                return NotFound();

            return View(vehicle);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var vehicle = _context.Vehicles.Find(id);
            if (vehicle == null)
                return NotFound();

            var customerId = vehicle.CustomerId;

            _context.Vehicles.Remove(vehicle);
            _context.SaveChanges();

            return RedirectToAction("Index", "Vehicle", new { id = customerId });
        }

        public IActionResult Details(Guid id)
        {
            var vehicle = _context.Vehicles
                .Include(v => v.Customer)
                .FirstOrDefault(v => v.Id == id);

            if (vehicle == null)
                return NotFound();

            return View(vehicle);
        }


    }


}