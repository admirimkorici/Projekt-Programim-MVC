using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Programim_MVC.Models
{
    public class Rezervimet
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Rezervimi")]
        public DateTime Date_Rezervimi { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Kthimi")]
        public DateTime Date_kthimi { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Pagesa_totale { get; set; }
        [Required]
        [Display(Name = "Numer Telefoni")]
        public string NumerTelefoni { get; set; }
        [Required]
        public string Adresa { get; set; }

        [Display(Name = "Makina")]
        public int? MakinatID { get; set; }
        public virtual Makina Makinat { get; set; }
        public string UserId { get; set; }
    }
}
