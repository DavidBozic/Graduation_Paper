using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ForgAkademija.Models
{
    public class Bill
    {
        [Key]
        public int billId { get; set; }

        [RegularExpression(@"\d{16}", ErrorMessage = "Unesite validan broj kartice.")]
        [Display(Name = "Broj kartice")]
        [Required(ErrorMessage = "Broj kartice je obavezan")]
        public long cardNumber { get; set; }

        [RegularExpression(@"\d{2}[/]\d{2}", ErrorMessage = "Unesite validan rok vazenja kartice, format za unos je 'xx/xx'")]
        [Display(Name = "Rok vazenja kartice")]
        [Required(ErrorMessage = "Rok vazenja kartice je obavezan")]
        public string cardValidityPeriod { get; set; }

        [RegularExpression(@"\d{3}", ErrorMessage = "Unesite validan CVC kod kartice (trocifreni broj na poledjini)")]
        [Display(Name = "CVC kod")]
        [Required(ErrorMessage = "CVC kod kartice je obavezan")]
        public int cvcCode { get; set; }

        public bool isDeleted { get; set; }
        public int courseId { get; set; }
        public string userId { get; set; }
    }
}
