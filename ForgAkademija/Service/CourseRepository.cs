using ForgAkademija.Areas.Identity.Data;
using ForgAkademija.Infrastructure;
using ForgAkademija.Models;
using ForgAkademija.ViewModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ForgAkademija.Service
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CourseRepository(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public int CountCourses()
        {
            var sum = _context.Course.Where(c => c.isDeleted == false).ToList();
            return sum.Count();
        }

        public void DeleteCourse(int id)
        {
            //Deleting bills of the selected course

            List<Bill> billList = _context.Bill.Where(b => b.courseId == id && b.isDeleted == false).ToList();

            foreach (var item in billList)
            {
                item.isDeleted = true;
                _context.Update(item);
            }
            _context.SaveChanges();

            //Deleting comments of the selected course

            List<Comment> commentsList = _context.Comment.Where(c => c.courseId == id && c.isDeleted == false).ToList();

            foreach (var item in commentsList)
            {
                item.isDeleted = true;
                _context.Update(item);
            }
            _context.SaveChanges();

            ////////////////////////////////////////

            Course course = _context.Course.Where(c => c.courseId == id).FirstOrDefault();
            course.isDeleted = true;

            _context.Course.Update(course);
            _context.SaveChanges();
        }

        public void InsertCourse(CourseViewModel courseVM, string userId)
        {
            Course course = new Course();

            course.courseName = courseVM.courseName;
            course.courseDescription = courseVM.courseDescription;
            course.courseDuration = Convert.ToSingle(courseVM.courseDuration);
            course.coursePrice = Convert.ToSingle(courseVM.coursePrice);
            course.videoLink = courseVM.videoLink;
            course.additionalMaterialUrl = courseVM.additionalMaterialUrl;
            course.logoUrl = courseVM.logoUrl;
            course.creationTime = DateTime.Now;
            course.creatorId = userId;
            course.isDeleted = false;

            _context.Course.Add(course);
            _context.SaveChanges();
        }

        public List<Course> ReturnAllCourses()
        {
            return _context.Course.Where(c => c.isDeleted == false).ToList();
        }

        public Course ReturnCourse(int id)
        {
            return _context.Course.Where(c => c.courseId == id).FirstOrDefault();
        }

        public void UpdateCourse(Course course)
        {
            _context.Course.Update(course);
            _context.SaveChanges();
        }
    }
}
