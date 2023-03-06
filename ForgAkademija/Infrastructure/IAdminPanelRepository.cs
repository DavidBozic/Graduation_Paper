using ForgAkademija.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace ForgAkademija.Infrastructure
{
    public interface IAdminPanelRepository
    {
        int CountModerators();
        int CountUsers();
        List<UsersWithRolesViewModel> ReturnUsersWithRoles();
    }
}
