using System.Collections.Generic;
using SimbioMed.Authorization.Permissions.Dto;

namespace SimbioMed.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}