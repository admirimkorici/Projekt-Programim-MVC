using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rezervimet.Include(r => r.Makinat);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Rezervimet/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Rezervimet/Create
        public IActionResult Create()
        {
            ViewData["MakinatID"] = new SelectList(_context.Makina, "ID", "Modeli");
            return View();
        }

        // POST: Rezervimet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Date_Rezervimi,Date_kthimi,Pagesa_totale,NumerTelefoni,Adresa,MakinatID")] Rezervimet rezervimet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rezervimet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MakinatID"] = new SelectList(_context.Makina, "ID", "Modeli", rezervimet.MakinatID);
            return View(rezervimet);
        }

        // GET: Rezervimet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezervimet = await _context.Rezervimet.FindAsync(id);
            if (rezervimet == null)
            {
                return NotFound();
            }
            ViewData["MakinatID"] = new SelectList(_context.Makina, "ID", "Modeli", rezervimet.MakinatID);
            return View(rezervimet);
        }

        // POST: Rezervimet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Date_Rezervimi,Date_kthimi,Pagesa_totale,NumerTelefoni,Adresa,MakinatID")] Rezervimet rezervimet)
        {
            if (id != rezervimet.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezervimet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezervimetExists(rezervimet.ID))
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
            ViewData["MakinatID"] = new SelectList(_context.Makina, "ID", "Modeli", rezervimet.MakinatID);
            return View(rezervimet);
        }

        // GET: Rezervimet/Delete/5
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rezervimet = await _context.Rezervimet.FindAsync(id);
            _context.Rezervimet.Remove(rezervimet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezervimetExists(int id)
        {
            return _context.Rezervimet.Any(e => e.ID == id);
        }
    }
}
