using ForgAkademija.Infrastructure;
using ForgAkademija.Models;
using ForgAkademija.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ForgAkademija.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _course;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IBillRepository _bill;

        public CourseController(ICourseRepository course, UserManager<IdentityUser> userManager, IWebHostEnvironment webHostEnvironment, IBillRepository bill)
        {
            _course = course;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _bill = bill;
        }

        #region Return course and course items
        [AllowAnonymous]
        [HttpGet]
        public IActionResult CoursesOverview()
        {
            List<Course> courses = _course.ReturnAllCourses();
            ViewBag.numOfCourses = _course.CountCourses();
            return View(courses);
        }
        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult CourseDetails(int id)
        {
            Course course = _course.ReturnCourse(id);
            ViewData["billCorrectness"] = _bill.CheckBill(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(course);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Video(int id)
        {
            Course course = _course.ReturnCourse(id);
            return View(course);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AdditionalMaterial(int id)
        {
            Course course = _course.ReturnCourse(id);

            var path = "wwwroot" + course.additionalMaterialUrl;

            var memory = new MemoryStream();

            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            var contentType = "APPLICATION/octet-stream";
            var fileName = Path.GetFileName(path);

            return File(memory, contentType, fileName);
        }
        #endregion

        #region Adding course
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public IActionResult AddCourse()
        {
            CourseViewModel courseVM = new CourseViewModel();
            return View(courseVM);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public async Task<IActionResult> AddCourse(CourseViewModel courseVM)
        {
            try
            {
                /* Adding additional materials */
                if (courseVM.additionalMaterial != null)
                {
                    string folder = "course/additionalMaterials/";
                    folder += courseVM.additionalMaterial.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    courseVM.additionalMaterialUrl = "/" + folder;

                    await courseVM.additionalMaterial.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }

                /* Adding course icon */
                if (courseVM.logo != null)
                {
                    string folder = "course/icons/";
                    folder += courseVM.logo.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    courseVM.logoUrl = "/" + folder;

                    await courseVM.logo.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }

                _course.InsertCourse(courseVM, User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (User.IsInRole("Administrator"))
                {
                    return RedirectToAction("AdminPanel", "Account");
                }
                else
                {
                    return RedirectToAction("ModeratorPanel", "Account");
                }
            }
            catch (Exception ex)
            {
                return NotFound($"Došlo je do greške, detalji: {ex.Message}");
            }
        }
        #endregion

        #region Edit course
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public IActionResult EditCourse(int id)
        {
            Course course = _course.ReturnCourse(id);
            ViewBag.selectedCourse = course.courseName;
            return View(course);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public IActionResult EditCourse(Course course)
        {
            try
            {
                _course.UpdateCourse(course);

                if (User.IsInRole("Administrator"))
                {
                    return RedirectToAction("AdminPanel", "Account");
                }
                else
                {
                    return RedirectToAction("ModeratorPanel", "Account");
                }
            }
            catch (Exception ex)
            {
                return NotFound($"Došlo je do greške, detalji: {ex.Message}");
            }
        }
        #endregion

        #region Delete course
        [Authorize(Roles = "Administrator, Moderator")]
        [HttpGet]
        public IActionResult DeleteCourse(int id)
        {
            Course course = _course.ReturnCourse(id);
            return View(course);
        }

        [Authorize(Roles = "Administrator, Moderator")]
        [HttpPost]
        public IActionResult DeleteCourse(Course course)
        {
            try
            {
                var additionalMaterial = "wwwroot" + course.additionalMaterialUrl;
                var logo = "wwwroot" + course.logoUrl;
                FileInfo fileInfo = new FileInfo(additionalMaterial);

                /* Deleting additional materials */
                if (fileInfo != null)
                {
                    System.IO.File.Delete(additionalMaterial);
                    fileInfo.Delete();
                }

                fileInfo = new FileInfo(logo);

                /* Deleting course icon */
                if (fileInfo != null)
                {
                    System.IO.File.Delete(logo);
                    fileInfo.Delete();
                }

                _course.DeleteCourse(course.courseId);

                if (User.IsInRole("Administrator"))
                {
                    return RedirectToAction("AdminPanel", "Account");
                }
                else
                {
                    return RedirectToAction("ModeratorPanel", "Account");
                }
            }
            catch (Exception ex)
            {
                return NotFound($"Došlo je do greške, detalji: {ex.Message}");
            }
        }
        #endregion
    }
}
