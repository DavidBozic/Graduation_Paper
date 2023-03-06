using ForgAkademija.Infrastructure;
using ForgAkademija.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ForgAkademija.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _comment;
        private readonly UserManager<IdentityUser> _userManager;

        public CommentController(ICommentRepository comment, UserManager<IdentityUser> userManager)
        {
            _comment = comment;
            _userManager = userManager;
        }

        #region Add Comment
        [HttpGet]
        public IActionResult AddComment(int id)
        {
            Comment comment = new Comment()
            {
                courseId = id
            };

            return View(comment);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            try
            {
                _comment.InsertComment(comment, await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)));

                return RedirectToAction("CourseDetails", "Course", new { Id = comment.courseId });
            }
            catch (Exception ex)
            {
                return NotFound($"Došlo je do greške, detalji: {ex.Message}");
            }
        }
        #endregion

        #region Delete Comment
        [HttpGet]
        public async Task<IActionResult> DeleteComment(int id)
        {
            Comment comment = _comment.ReturnComment(id);

            try
            {
                _comment.DeleteComment(comment);

                return RedirectToAction("CourseDetails", "Course", new { id = comment.courseId });
            }
            catch (Exception ex)
            {
                return NotFound($"Došlo je do greške, detalji: {ex.Message}");
            }
        }
        #endregion
    }
}
