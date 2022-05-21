using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Programim_MVC.Models
{
    public class Mesazhe
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} eshte e i detyrueshem!")]
        public string Emri { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "{0} eshte i detyrueshem!")]
        public string Email { get; set; }
        public string Subjekti { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "{0} eshte i detyrueshem!")]
        public string Mesazhi { get; set; }
        [Required]
        public DateTime Koha { get; set; }
        [Required]
        public bool Statusi { get; set; }
    }
}
