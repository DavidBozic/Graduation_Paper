using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ForgAkademija.ViewModel
{
    public class UsersWithRolesViewModel
    {
        public string userId { get; set; }

        [Display(Name = "Korisničko ime")]
        [Required(ErrorMessage = "Korisničko ime je obavezno.")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Email adresa je obavezna.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }

        public string? role { get; set; }
    }
}
