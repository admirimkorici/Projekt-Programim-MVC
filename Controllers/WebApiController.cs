using Projekt_Programim_MVC.Data;
using Projekt_Programim_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Programim_MVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WebApiController : ControllerBase
    {
        private ApplicationDbContext _context;
        public WebApiController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Mesazhe>))]
        public IActionResult Index()
        {
            return Ok(_context.Mesazhe.Where(m => m.Statusi == false));
        }
    }
}
