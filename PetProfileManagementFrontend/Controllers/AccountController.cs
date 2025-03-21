using Microsoft.AspNetCore.Mvc;
using PetProfileManagementFrontend.Models;

namespace PetProfileManagementFrontend.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Hardcoded Static Login (Replace with database check if needed)
                if (model.Email == "admin@example.com" && model.Password == "admin123")
                {
                    return RedirectToAction("Index", "Pet"); // Redirects to Pet Index Page
                }
                else
                {
                    model.ErrorMessage = "Invalid email or password.";
                    return View(model);
                }
            }

            return View(model);
        }
    }
}
