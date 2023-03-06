using System.ComponentModel.DataAnnotations;

namespace ForgAkademija.ViewModel
{
    public class CourseViewModel
    {
        [Key]
        public int courseId { get; set; }

        [Required(ErrorMessage = "Polje za naziv kursa je obavezno.")]
        [StringLength(30, ErrorMessage = "Naziv kursa može da ima najviše 30 karaktera.")]
        [Display(Name = "Naziv kursa")]
        public string courseName { get; set; }

        [Required(ErrorMessage = "Polje za opis kursa je obavezno.")]
        [StringLength(1000, ErrorMessage = "Opis kursa može da ima najviše 1000 karaktera.")]
        [Display(Name = "Opis kursa")]
        public string courseDescription { get; set; }

        [Required(ErrorMessage = "Polje za trajanje kursa je obavezno.")]
        [RegularExpression(@"\d+", ErrorMessage = "Unesite validno vreme trajanja kursa.")]
        [Display(Name = "Trajanje kursa")]
        public string courseDuration { get; set; }

        [Required(ErrorMessage = "Polje za cenu kursa je obavezno.")]
        [Range(1, 2000, ErrorMessage = "Cena kursa ne može biti manja od 1, ni veća od 2000.")]
        [Display(Name = "Cena")]
        public string coursePrice { get; set; }

        [Required(ErrorMessage = "Polje za link videa je obavezno.")]
        [RegularExpression(@"[(http(s)?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)", ErrorMessage = "Unesite validan link videa.")]
        [Display(Name = "Link videa")]
        public string videoLink { get; set; }

        [Required(ErrorMessage = "Dodatni materijal je obavezan.")]
        [Display(Name = "Dodatni materijal")]
        public IFormFile additionalMaterial { get; set; }

        public string additionalMaterialUrl { get; set; }

        [Required(ErrorMessage = "Ikonica kursa je obavezna.")]
        [Display(Name = "Ikonica kursa")]
        public IFormFile logo { get; set; }

        public string logoUrl { get; set; }

        public string? searchTerm { get; set; }
    }
}
