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
    public class MesazheController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MesazheController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Mesazhe
        public async Task<IActionResult> Index()
        {
            return View(await _context.Mesazhe.ToListAsync());
        }
        // GET: Mesazhe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var mesazhe = await _context.Mesazhe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mesazhe == null)
            {
                return NotFound();
            }
            else
            {
                if (mesazhe.Statusi == false)
                {
                    mesazhe.Statusi = true;
                    _context.SaveChanges();
                }
            }
            return View(mesazhe);
        }

        // GET: Mesazhe/Create
        [Route("Mesazhe/New/")]
        public IActionResult Create()
        {
            return View();
        }
        // POST: Mesazhe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Mesazhe/New/")]
        public async Task<IActionResult> Create(Mesazhe mesazhe)
        {
            if (ModelState.IsValid)
            {
                _context.Mesazhe.Add(new Mesazhe { Emri = mesazhe.Emri, Email = mesazhe.Email, Subjekti = mesazhe.Subjekti, Mesazhi = mesazhe.Mesazhi, Koha = DateTime.Now, Statusi = false });
                await _context.SaveChangesAsync();
                ViewBag.Msg = "Mesazhi u dergua!";
            }
            return View(mesazhe);
        }
        // GET: Mesazhe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var mesazhe = await _context.Mesazhe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mesazhe == null)
            {
                return NotFound();
            }
            return View(mesazhe);
        }
        // POST: Mesazhe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mesazhe = await _context.Mesazhe.FindAsync(id);
            _context.Mesazhe.Remove(mesazhe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool MesazheExists(int id)
        {
            return _context.Mesazhe.Any(e => e.Id == id);
        }
    }
}
