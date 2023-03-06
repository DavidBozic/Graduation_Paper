using ForgAkademija.Infrastructure;
using ForgAkademija.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForgAkademija.ViewComponents
{
    public class CoursesTableViewComponent : ViewComponent
    {
        private readonly ICourseRepository _course;

        public CoursesTableViewComponent(ICourseRepository course)
        {
            _course = course;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Course> courses = _course.ReturnAllCourses();
            return View(courses);
        }
    }
}
