using ForgAkademija.Infrastructure;
using ForgAkademija.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ForgAkademija.Controllers
{
    [Authorize(Roles = "User")]
    public class BillController : Controller
    {
        private readonly IBillRepository _bill;
        private readonly ICourseRepository _course;
        private readonly UserManager<IdentityUser> _userManager;

        public BillController(IBillRepository bill, ICourseRepository course, UserManager<IdentityUser> userManager)
        {
            _bill = bill;
            _course = course;
            _userManager = userManager;
        }

        #region Add Bill
        [HttpGet]
        public IActionResult AddBill(int id)
        {
            Bill bill = new Bill();
            bill.courseId = id;
            return View(bill);
        }

        [HttpPost]
        public IActionResult AddBill(Bill bill)
        {
            try
            {
                _bill.InsertBill(bill, User.FindFirstValue(ClaimTypes.NameIdentifier));
                return RedirectToAction("CourseDetails","Course", new { id = bill.courseId });
            }
            catch (Exception ex)
            {
                return NotFound($"Došlo je do greške, detalji: {ex.Message}");
            }
        }
        #endregion

        #region Delete Bill
        [HttpGet]
        public IActionResult DeleteBill(int id)
        {
            Bill bill = _bill.CheckBill(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            ViewData["courseName"] = _course.ReturnCourse(bill.courseId).courseName;
            return View(bill);
        }

        [HttpPost]
        public IActionResult DeleteBill(Bill bill)
        {
            try
            {
                _bill.DeleteBill(bill);
                return RedirectToAction("CourseDetails", "Course", new { id = bill.courseId });
            }
            catch (Exception ex)
            {
                return NotFound($"Došlo je do greške, detalji: {ex.Message}");
            }
        }
        #endregion
    }
}
