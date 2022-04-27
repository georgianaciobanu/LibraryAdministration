using System.Threading.Tasks;
using Abp.Application.Services;
using SimbioMed.Configuration.Tenants.Dto;

namespace SimbioMed.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
