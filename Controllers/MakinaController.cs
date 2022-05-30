using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt_Programim_MVC.Data;
using Projekt_Programim_MVC.Models;

namespace Projekt_Programim_MVC.Controllers
{
    public class MakinaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MakinaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Makina
        public async Task<IActionResult> Index(string? marka)
        {
            var applicationDbContext = await _context.Makina.Include(m => m.Tipi).ToListAsync();
            if (!String.IsNullOrEmpty(marka))
            {
                applicationDbContext = await _context.Makina.Where(m => m.Tipi.Emri == marka).ToListAsync();
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddHours(2);
                options.Secure = true;
                Response.Cookies.Append("Marka", marka, options);
            }
            return View(applicationDbContext);
        }

        // GET: Makina/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var makina = await _context.Makina
                .Include(m => m.Tipi)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (makina == null)
            {
                return NotFound();
            }

            return View(makina);
        }

        // GET: Makina/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["TipiID"] = new SelectList(_context.Tipi, "ID", "Emri");
            return View();
        }

        // POST: Makina/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ID,Modeli,Pershkrimi,Vit_Prodhimi,ERezervuar,Kosto1Dite,Foto,TipiID")] Makina makina)
        {
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileNameWithoutExtension(makina.Foto.FileName);
                string extension = Path.GetExtension(makina.Foto.FileName);
                makina.IMG = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine("wwwroot/Images/Uploaded/", filename);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await makina.Foto.CopyToAsync(fileStream);
                }
                _context.Add(makina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipiID"] = new SelectList(_context.Tipi, "ID", "Emri", makina.TipiID);
            return View(makina);
        }

        // GET: Makina/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var makina = await _context.Makina.FindAsync(id);
            if (makina == null)
            {
                return NotFound();
            }
            ViewData["TipiID"] = new SelectList(_context.Tipi, "ID", "Emri", makina.TipiID);
            return View(makina);
        }

        // POST: Makina/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Modeli,Pershkrimi,Vit_Prodhimi,ERezervuar,Kosto1Dite,IMG,TipiID")] Makina makina)
        {
            if (id != makina.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(makina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MakinaExists(makina.ID))
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
            ViewData["TipiID"] = new SelectList(_context.Tipi, "ID", "Emri", makina.TipiID);
            return View(makina);
        }

        // GET: Makina/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var makina = await _context.Makina
                .Include(m => m.Tipi)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (makina == null)
            {
                return NotFound();
            }

            return View(makina);
        }

        // POST: Makina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var makina = await _context.Makina.FindAsync(id);
            if (makina.IMG != null)
            {
                var imagePath = Path.Combine("wwwroot/Images/Uploaded/", makina.IMG);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
            }
            _context.Makina.Remove(makina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MakinaExists(int id)
        {
            return _context.Makina.Any(e => e.ID == id);
        }
    }
}
