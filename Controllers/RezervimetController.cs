using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt_Programim_MVC.Data;
using Projekt_Programim_MVC.Models;

namespace Projekt_Programim_MVC.Controllers
{
    public class RezervimetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RezervimetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rezervimet
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applicationDbContext = _context.Rezervimet.Include(r => r.Makinat).Where(m => m.UserId == userId);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize]
        public async Task<IActionResult> AllReservation()
        {
            var result = await _context.Rezervimet.Include(m => m.Makinat).ToListAsync();
            return View(result);
        }

        // GET: Rezervimet/Create
        [Authorize]
        public IActionResult Create(int? Mid)
        {
            var result = _context.Makina.Where(m => m.ID == Mid).SingleOrDefault();
            if (!result.ERezervuar)
            {
                ViewBag.msg = "Error";
            }
            else
            {
                ViewBag.msg = result.Modeli;
                ViewBag.Kosto = result.Kosto1Dite;
                ViewBag.Id = Mid;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create(Rezervimet rezervimet, int id, decimal kosto)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                DateTime dt1 = rezervimet.Date_Rezervimi;
                DateTime dt2 = rezervimet.Date_kthimi;
                TimeSpan span = dt2.Subtract(dt1);
                var result = span.Days;
                var pagesa = result * kosto;
                _context.Add(new Rezervimet { Date_Rezervimi = rezervimet.Date_Rezervimi, Date_kthimi = rezervimet.Date_kthimi, Pagesa_totale = pagesa, NumerTelefoni = rezervimet.NumerTelefoni, Adresa = rezervimet.Adresa, MakinatID = id, UserId = userId});
                var makine = _context.Makina.Where(m => m.ID == id).SingleOrDefault();
                makine.ERezervuar = false;
                _context.SaveChanges();
                ViewBag.mesazh = "Makina u rezervua!";
            }
            return View(rezervimet);
        }

        // GET: Rezervimet/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervimet = await _context.Rezervimet
                .Include(r => r.Makinat)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rezervimet == null)
            {
                return NotFound();
            }

            return View(rezervimet);
        }

        // POST: Rezervimet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rezervimet = await _context.Rezervimet.FindAsync(id);
            var rezervim = await _context.Rezervimet.Where(m => m.ID == id).SingleOrDefaultAsync();
            var makine = rezervim.MakinatID;
            var result = await _context.Makina.Where(m => m.ID == makine).SingleOrDefaultAsync();
            result.ERezervuar = true;
            _context.Rezervimet.Remove(rezervimet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Confirm));
        }
        public IActionResult Confirm()
        {
            return View();
        }

        private bool RezervimetExists(int id)
        {
            return _context.Rezervimet.Any(e => e.ID == id);
        }
    }
}
