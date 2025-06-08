using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Warsztat_samochodowy.Models;
using Warsztat_samochodowy.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace Warsztat_samochodowy.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUserModel> _userManager;
        private readonly SignInManager<ApplicationUserModel> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<ApplicationUserModel> userManager,
            SignInManager<ApplicationUserModel> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminRegister()
        {
            // Pobieramy nazwy ról z bazy i przekazujemy do widoku
            var roles = _roleManager.Roles.Select(r => r.Name).ToList();
            ViewBag.Roles = roles;

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminRegister(AdminRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Gdy walidacja nie przejdzie, też musimy przekazać listę ról
                ViewBag.Roles = _roleManager.Roles.Select(r => r.Name).ToList();
                return View(model);
            }

            var user = new ApplicationUserModel
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (!await _userManager.IsInRoleAsync(user, model.Role))
                    await _userManager.AddToRoleAsync(user, model.Role);

                TempData["Success"] = "Użytkownik został dodany.";
                return RedirectToAction("AdminRegister");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            // W przypadku błędów też zwracamy listę ról do widoku
            ViewBag.Roles = _roleManager.Roles.Select(r => r.Name).ToList();

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("", "Nieprawidłowe dane logowania.");
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}