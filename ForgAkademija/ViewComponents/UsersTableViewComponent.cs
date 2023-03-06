using ForgAkademija.Infrastructure;
using ForgAkademija.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ForgAkademija.ViewComponents
{
    public class UsersTableViewComponent : ViewComponent
    {
        private readonly IAdminPanelRepository _adminPanelRepository;

        public UsersTableViewComponent(IAdminPanelRepository adminPanelRepository)
        {
            _adminPanelRepository = adminPanelRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<UsersWithRolesViewModel> users = _adminPanelRepository.ReturnUsersWithRoles();
            return View(users);
        }
    }
}
