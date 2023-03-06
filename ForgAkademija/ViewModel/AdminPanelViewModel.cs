using ForgAkademija.Models;
using Microsoft.AspNetCore.Identity;

namespace ForgAkademija.ViewModel
{
    public class AdminPanelViewModel
    {
        public IdentityUser user { get; set; }
        public int numOfModerators { get; set; }
        public int numOfUsers { get; set; }
        public int numOfCourses { get; set; }
    }
}
