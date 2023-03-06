using ForgAkademija.Models;
using ForgAkademija.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace ForgAkademija.Infrastructure
{
    public interface ICourseRepository
    {
        Course ReturnCourse(int id);
        List<Course> ReturnAllCourses();
        void InsertCourse(CourseViewModel courseVM, string userId);
        void UpdateCourse(Course course);
        void DeleteCourse(int id);
        int CountCourses();
    }
}
