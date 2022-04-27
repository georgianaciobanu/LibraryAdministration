using Abp.Authorization;
using SimbioMed.Authorization.Roles;
using SimbioMed.Authorization.Users;

namespace SimbioMed.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
