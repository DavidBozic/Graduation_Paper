using ForgAkademija.Areas.Identity.Data;
using ForgAkademija.Infrastructure;
using ForgAkademija.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace ForgAkademija.Service
{
    public class AdminPanelRepository : IAdminPanelRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminPanelRepository(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public int CountModerators()
        {
            return _userManager.GetUsersInRoleAsync("moderator").Result.Count;
        }

        public int CountUsers()
        {
            return _userManager.Users.ToList().Count;
        }

        public List<UsersWithRolesViewModel> ReturnUsersWithRoles()
        {
            List<UsersWithRolesViewModel> users = new List<UsersWithRolesViewModel>();

            users = _userManager.Users.Select(user => new UsersWithRolesViewModel
            {
                userId = user.Id,
                userName = user.UserName,
                email = user.Email,
                role =  _userManager.GetRolesAsync(user).Result.FirstOrDefault()
            }).ToList();

            return users;
        }
    }
}
