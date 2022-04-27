using System.Threading.Tasks;
using Abp.Application.Services;
using SimbioMed.Install.Dto;

namespace SimbioMed.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}