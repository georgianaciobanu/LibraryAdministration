using System.Collections.Generic;
using MvvmHelpers;
using SimbioMed.Models.NavigationMenu;

namespace SimbioMed.Services.Navigation
{
    public interface IMenuProvider
    {
        ObservableRangeCollection<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}