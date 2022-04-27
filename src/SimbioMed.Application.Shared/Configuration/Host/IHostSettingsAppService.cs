using System.Threading.Tasks;
using Abp.Application.Services;
using SimbioMed.Configuration.Host.Dto;

namespace SimbioMed.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
