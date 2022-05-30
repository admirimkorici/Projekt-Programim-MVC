using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Projekt_Programim_MVC.Data;
using Projekt_Programim_MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Programim_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var result = _context.Tipi.ToList();
            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Search(string? q)
        {
            var result = _context.Makina.ToList();
            if (!String.IsNullOrEmpty(q))
            {
                result = _context.Makina.Where(m => m.Modeli.Contains(q)).ToList();
                if (result.Count() == 0)
                {
                    ViewBag.msg = "Nuk u gjend asnje rezultat!";
                }
            }
            ViewBag.msg2 = q;
            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
