using ForgAkademija.Infrastructure;
using ForgAkademija.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForgAkademija.ViewComponents
{
    public class CommentsListViewComponent : ViewComponent
    {
        private readonly ICommentRepository _comment;

        public CommentsListViewComponent(ICommentRepository comment)
        {
            _comment = comment;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            List<Comment> courseComments = _comment.ReturnAllComments(id);
            return View(courseComments);
        }
    }
}
