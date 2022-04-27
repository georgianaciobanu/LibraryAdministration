using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SimbioMed.Authorization.Permissions.Dto;

namespace SimbioMed.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
