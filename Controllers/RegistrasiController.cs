using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Musclegym.Data;
using Musclegym.Migrations;
using Musclegym.Models; // Pastikan untuk mengimpor namespace yang benar

namespace Musclegym.Controllers
{
    public class RegistrasiController : Controller
    {
        private readonly MusclegymContext _context; // Pastikan penggunaan DataContext yang sesuai

        public RegistrasiController(MusclegymContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Registrasi()
        {
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrasi([Bind("Id,Email,Password,Confirm_Password")] Registrasi registrasi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registrasi);
                await _context.SaveChangesAsync();
                return RedirectToAction("login", "Home");
            }
            return View(registrasi);
        }
    }
}



