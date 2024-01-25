using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Musclegym.Data;
using Musclegym.Models;

namespace Musclegym.Controllers
{
    public class MembersController : Controller
    {
        private readonly MusclegymContext _context;

        public MembersController(MusclegymContext context)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
              return _context.Member != null ? 
                          View(await _context.Member.ToListAsync()) :
                          Problem("Entity set 'MusclegymContext.Member'  is null.");
        }

        public async Task<IActionResult> Search(string searchString)
        {
            var members = from m in _context.Member
                          select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                // Filter untuk string (nama, email, dan lainnya)
                members = members.Where(s =>
                    EF.Functions.Like(s.Nama, $"%{searchString}%") ||
                    EF.Functions.Like(s.JenisKelamin, $"%{searchString}%") ||
                    EF.Functions.Like(s.Kelas, $"%{searchString}%") ||
                    EF.Functions.Like(s.Cabang, $"%{searchString}%") 
               
                );

               

                // Filter untuk datetime (tanggal transaksi)
                DateTime dateTime;
                if (DateTime.TryParse(searchString, out dateTime))
                {
                    members = members.Where(s =>
                        s.TanggalTransaksi == dateTime
                    // Tambahkan kriteria pencarian datetime lainnya di sini
                    );
                }

                // Filter untuk date (hanya tanggal, tanpa memperhitungkan waktu)
                DateTime date;
                if (DateTime.TryParse(searchString, out date))
                {
                    members = members.Where(s =>
                        s.TanggalLahir.Date == date.Date
                   
                    );
                }

                // Filter untuk email address
                members = members.Where(s =>
                    IsValidEmail(searchString) && s.Email == searchString
                // Menggunakan IsValidEmail untuk memvalidasi alamat email
                );
            }

            return View("Index", await members.AsNoTracking().ToListAsync());
        }


        // Method untuk memvalidasi alamat email
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,TanggalTransaksi,Nama,Email,NoHp,TanggalLahir,JenisKelamin,Kelas,Cabang,TanggalBergabung,Paket,StatusMember")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Jika ModelState tidak valid, maka perlu mengambil data untuk dropdown
            ViewData["CabangList"] = GetCabangList();
            ViewData["PaketList"] = GetPaketList();
            ViewData["StatusMemberList"] = GetStatusMemberList();

            return View(member);
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


        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,TanggalTransaksi,Nama,Email,NoHp,TanggalLahir,JenisKelamin,Kelas,Cabang")] Member member)
        {
            if (id != member.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Member == null)
            {
                return Problem("Entity set 'MusclegymContext.Member'  is null.");
            }
            var member = await _context.Member.FindAsync(id);
            if (member != null)
            {
                _context.Member.Remove(member);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
          return (_context.Member?.Any(e => e.MemberId == id)).GetValueOrDefault();
        }
    }
}
