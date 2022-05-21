using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Programim_MVC.Models
{
    public class Tipi
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Tipi")]
        public string Emri { get; set; }
        [Display(Name = "Ikona")]
        public string Ikona { get; set; }
        [NotMapped]
        [Display(Name = "Ngarko Foto")]
        public IFormFile Foto { get; set; }
    }
}
