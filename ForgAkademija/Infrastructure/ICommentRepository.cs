using ForgAkademija.Models;
using Microsoft.AspNetCore.Identity;

namespace ForgAkademija.Infrastructure
{
    public interface ICommentRepository
    {
        void InsertComment(Comment comment, IdentityUser user);
        void DeleteComment(Comment comment);
        Comment ReturnComment(int commentId);
        List<Comment> ReturnAllComments(int courseId);
    }
}
