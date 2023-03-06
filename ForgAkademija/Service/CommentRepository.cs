using ForgAkademija.Areas.Identity.Data;
using ForgAkademija.Infrastructure;
using ForgAkademija.Models;
using Microsoft.AspNetCore.Identity;

namespace ForgAkademija.Service
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DeleteComment(Comment comment)
        {
            comment.isDeleted = true;

            _context.Update(comment);
            _context.SaveChanges();
        }

        public void InsertComment(Comment comment, IdentityUser user)
        {
            comment.userId = user.Id;
            comment.userName = user.UserName;
            comment.createdDate = DateTime.Now;
            comment.isDeleted = false;

            _context.Comment.Add(comment);
            _context.SaveChanges();
        }

        public List<Comment> ReturnAllComments(int courseId)
        {
            return _context.Comment.Where(c => c.courseId == courseId && c.isDeleted == false).ToList();
        }

        public Comment ReturnComment(int commentId)
        {
            return _context.Comment.Where(c=>c.commentId == commentId && c.isDeleted == false).FirstOrDefault();
        }
    }
}
