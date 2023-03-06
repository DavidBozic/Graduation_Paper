using ForgAkademija.Areas.Identity.Data;
using ForgAkademija.Infrastructure;
using ForgAkademija.Models;
using ForgAkademija.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ForgAkademija.Controllers
{
    [Authorize(Roles = "Administrator, Moderator")]
    public class AccountController : Controller
    {
        private readonly ICourseRepository _course;
        private readonly IAdminPanelRepository _adminPanelRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public AccountController(ICourseRepository course, IAdminPanelRepository adminPanelRepository,
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _course = course;
            _adminPanelRepository = adminPanelRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        #region Panels
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> AdminPanel()
        {
            AdminPanelViewModel adminVM = new AdminPanelViewModel()
            {
                user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                numOfModerators = _adminPanelRepository.CountModerators(),
                numOfUsers = _adminPanelRepository.CountUsers(),
                numOfCourses = _course.CountCourses()
            };

            return View(adminVM);
        }

        [Authorize(Roles = "Moderator")]
        [HttpGet]
        public IActionResult ModeratorPanel()
        {
            return View();
        }
        #endregion

        #region Manage Users Roles
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> ManageRole(string id)
        {
            ViewBag.userId = id;

            var user = await _userManager.FindByIdAsync(id);
            var model = new List<ManageRolesViewModel>();

            foreach (var role in _roleManager.Roles)
            {
                var usersRoleModel = new ManageRolesViewModel()
                {
                    roleId = role.Id,
                    roleName = role.Name
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    usersRoleModel.isSelected = true;
                }
                else
                {
                    usersRoleModel.isSelected = false;
                }

                model.Add(usersRoleModel);
            }

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> ManageRole(List<ManageRolesViewModel> model)
        {
            var user = await _userManager.FindByIdAsync(model.FirstOrDefault().userId);

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Nije moguće ukloniti korisniku ulogu.");
                return View(model);
            }

            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.isSelected).Select(y => y.roleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Nije moguće dodati korisniku ulogu.");
                return View(model);
            }

            return RedirectToAction("AdminPanel", "Account");
        }
        #endregion

        #region Delete User
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            return View(user);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser(IdentityUser user)
        {
            try
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    /* Deleting all user bills */
                    foreach (Bill bill in _context.Bill.Where(b => b.userId == user.Id).ToList())
                    {
                        bill.isDeleted = true;
                        _context.Update(bill);
                        _context.SaveChanges();
                    }

                    return RedirectToAction("AdminPanel", "Account");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                return NotFound($"Došlo je do greške, detalji: {ex.Message}");
            }

            return View();
        }
        #endregion
    }
}