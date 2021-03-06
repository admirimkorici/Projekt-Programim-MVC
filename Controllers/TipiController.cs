using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt_Programim_MVC.Data;
using Projekt_Programim_MVC.Models;

namespace Projekt_Programim_MVC.Controllers
{
    public class TipiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tipi/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ID,Emri,Foto")] Tipi tipi)
        {
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileNameWithoutExtension(tipi.Foto.FileName);
                string extension = Path.GetExtension(tipi.Foto.FileName);
                tipi.Ikona = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine("wwwroot/Images/Uploaded/", filename);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await tipi.Foto.CopyToAsync(fileStream);
                }
                _context.Add(tipi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Confirm));
            }
            return View(tipi);
        }

        // GET: Tipi/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipi = await _context.Tipi.FindAsync(id);
            if (tipi == null)
            {
                return NotFound();
            }
            return View(tipi);
        }

        // POST: Tipi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Emri,Ikona")] Tipi tipi)
        {
            if (id != tipi.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipiExists(tipi.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Confirm));
            }
            return View(tipi);
        }

        // GET: Tipi/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipi = await _context.Tipi
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tipi == null)
            {
                return NotFound();
            }

            return View(tipi);
        }

        // POST: Tipi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipi = await _context.Tipi.FindAsync(id);
            if(tipi.Ikona != null)
            {
                var imagePath = Path.Combine("wwwroot/Images/Uploaded/", tipi.Ikona);
                if(System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
            }
            _context.Tipi.Remove(tipi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Confirm));
        }
        public IActionResult Confirm()
        {
            return View();
        }
        private bool TipiExists(int id)
        {
            return _context.Tipi.Any(e => e.ID == id);
        }
    }
}
