using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ForgAkademija.Models
{
    public class Comment
    {
        [Key]
        public int commentId { get; set; }

        [Display(Name = "Komentar")]
        [Required(ErrorMessage = "Ne možete ostaviti prazan komentar.")]
        public string commentText { get; set; }

        public DateTime createdDate { get; set; }
        public string userName { get; set; }
        public string userId { get; set; }
        public int courseId { get; set; }
        public bool isDeleted { get; set; }
    }
}
