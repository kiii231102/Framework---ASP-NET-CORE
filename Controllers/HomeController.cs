using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Musclegym.Data;
using Musclegym.Models;
using System.Diagnostics;

namespace Musclegym.Controllers
{
    public class HomeController : Controller
    {
        private readonly MusclegymContext _context;

        public HomeController(MusclegymContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Awal()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public async Task<IActionResult> Promo()
        {
            return _context.Member != null ?
                        View(await _context.Member.ToListAsync()) :
                        Problem("Entity set 'MusclegymContext.Member'  is null.");
        }

        public async Task<IActionResult> akun()
        {
            return _context.Registrasis != null ?
                        View(await _context.Registrasis.ToListAsync()) :
                        Problem("Entity set 'MusclegymContext.Registrasis'  is null.");
        }

        public IActionResult TabelPromo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TabelPromo([Bind("MemberId,TanggalTransaksi,Nama,Email,NoHp,TanggalLahir,JenisKelamin,Kelas,Cabang,TanggalBergabung,Paket,StatusMember")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();

                // Tambahkan pesan berhasil bergabung ke TempData
                TempData["SuccessMessage"] = "Berhasil bergabung!";

                return RedirectToAction(nameof(Index));
            }

            // Jika ModelState tidak valid, maka perlu mengambil data untuk dropdown
            ViewData["CabangList"] = GetCabangList();
            ViewData["PaketList"] = GetPaketList();
            ViewData["StatusMemberList"] = GetStatusMemberList();

            return View(member);
        }




        public IActionResult login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult login(Musclegym.Models.Registrasi Registrasis)
        {
            // Check if the login credentials match the admin credentials
            if (Registrasis.Email == "admin@gmail.com" && Registrasis.Password == "admin123")
            {
                TempData["IsAdmin"] = true;
                return RedirectToAction("admin", "Home");
            }

            // If not admin credentials, check in the database
            var user = _context.Registrasis.FirstOrDefault(u => u.Email == Registrasis.Email && u.Password == Registrasis.Password);

            if (user != null)
            {
                TempData["IsAdmin"] = true;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Log the entered email for debugging
                Console.WriteLine($"Login failed for email: {Registrasis.Email}");

                // Add a model error for general login failure
                ModelState.AddModelError(string.Empty, "Email or password is incorrect.");

                // Return the view with validation errors
                return View("login", Registrasis);
            }
        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,TanggalTransaksi,Nama,Email,NoHp,TanggalLahir,JenisKelamin,Kelas,Cabang,TanggalBergabung,Paket,StatusMember")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Berhasil bergabung!";

                return RedirectToAction(nameof(Index));
            }

            // Jika ModelState tidak valid, maka perlu mengambil data untuk dropdown
            ViewData["CabangList"] = GetCabangList();
            ViewData["PaketList"] = GetPaketList();
            ViewData["StatusMemberList"] = GetStatusMemberList();

            return View(member);
        }
        public async Task<IActionResult> admin()
        {
            var promos = await _context.Promos.ToListAsync();

            if (promos != null)
            {
                // Hitung total promosi
                int totalPromos = promos.Count;

                // Tambahkan informasi total promosi ke ViewData
                ViewData["TotalPromos"] = totalPromos;

                // Kirim model promos ke tampilan
                return View(promos);
            }
            else
            {
                return Problem("Entity set 'MusclegymContext.Promos' is null.");
            }
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Id,Nama,Email,NoHp,TanggalLahir,JenisKelamin,Durasi,Class,Cabang")] Promo promo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(promo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(promo);
        }

        // Metode untuk mendapatkan daftar Cabang
        private List<string> GetCabangList()
        {
            return new List<string> { "panam", "rumbai", "rambutan", "hangtuah", "dumai", "marpoyan" };
        }

        // Metode untuk mendapatkan daftar Paket
        private List<string> GetPaketList()
        {
            return new List<string> { "Paket 6 Bulan", "Paket 12 Bulan" };
        }

        // Metode untuk mendapatkan daftar StatusMember
        private List<string> GetStatusMemberList()
        {
            return new List<string> { "Sudah Member", "Belum Member" };
        }






      

        public IActionResult tables()
        {
            return View();
        }
      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}