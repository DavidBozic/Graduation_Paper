using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ForgAkademija.Models
{
    public class Course
    {
        [Key]
        public int courseId { get; set; }

        [StringLength(30, ErrorMessage = "Naziv kursa može da ima najviše 30 karaktera.")]
        [Display(Name = "Naziv kursa")]
        [Required(ErrorMessage = "Polje za naziv kursa je obavezno.")]
        public string courseName { get; set; }

        [StringLength(1000, ErrorMessage = "Opis kursa može da ima najviše 1000 karaktera.")]
        [Display(Name = "Opis kursa")]
        [Required(ErrorMessage = "Polje za opis kursa je obavezno.")]
        public string courseDescription { get; set; }

        [Display(Name = "Trajanje kursa")]
        [Required(ErrorMessage = "Polje za trajanje kursa je obavezno.")]
        public float courseDuration { get; set; }

        [Range(1, 2000, ErrorMessage = "Cena kursa ne može biti manja od 1, ni veća od 2000.")]
        [Display(Name = "Cena")]
        [Required(ErrorMessage = "Polje za cenu kursa je obavezno.")]
        public float coursePrice { get; set; }

        [RegularExpression(@"[(http(s)?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)", ErrorMessage = "Unesite validan link videa.")]
        [Display(Name = "Link videa")]
        [Required(ErrorMessage = "Polje za link videa kursa je obavezno.")]
        public string videoLink { get; set; }

        public string logoUrl { get; set; }
        public string additionalMaterialUrl { get; set; }
        public DateTime creationTime { get; set; }
        public bool isDeleted { get; set; }
        public string creatorId { get; set; }
    }
}
