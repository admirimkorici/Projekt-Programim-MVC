using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projekt_Programim_MVC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_Programim_MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Tipi> Tipi { get; set; }
        public DbSet<Makina> Makina { get; set; }
        public DbSet<Rezervimet> Rezervimet { get; set; }
        public DbSet<Mesazhe> Mesazhe { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
