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
    public class RegistrasisController : Controller
    {
        private readonly MusclegymContext _context;

        public RegistrasisController(MusclegymContext context)
        {
            _context = context;
        }

        // GET: Registrasis
        public async Task<IActionResult> Index()
        {
              return _context.Registrasis != null ? 
                          View(await _context.Registrasis.ToListAsync()) :
                          Problem("Entity set 'MusclegymContext.Registrasis'  is null.");
        }

        // GET: Registrasis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Registrasis == null)
            {
                return NotFound();
            }

            var registrasi = await _context.Registrasis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registrasi == null)
            {
                return NotFound();
            }

            return View(registrasi);
        }

        // GET: Registrasis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Registrasis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Password,Confirm_Password")] Registrasi registrasi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registrasi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(registrasi);
        }

        // GET: Registrasis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Registrasis == null)
            {
                return NotFound();
            }

            var registrasi = await _context.Registrasis.FindAsync(id);
            if (registrasi == null)
            {
                return NotFound();
            }
            return View(registrasi);
        }

        // POST: Registrasis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Password,Confirm_Password")] Registrasi registrasi)
        {
            if (id != registrasi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registrasi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrasiExists(registrasi.Id))
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
            return View(registrasi);
        }

        // GET: Registrasis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Registrasis == null)
            {
                return NotFound();
            }

            var registrasi = await _context.Registrasis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registrasi == null)
            {
                return NotFound();
            }

            return View(registrasi);
        }

        // POST: Registrasis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Registrasis == null)
            {
                return Problem("Entity set 'MusclegymContext.Registrasis'  is null.");
            }
            var registrasi = await _context.Registrasis.FindAsync(id);
            if (registrasi != null)
            {
                _context.Registrasis.Remove(registrasi);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrasiExists(int id)
        {
          return (_context.Registrasis?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
