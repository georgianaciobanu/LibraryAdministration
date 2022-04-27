using Abp.Application.Services;
using SimbioMed.Dto;
using SimbioMed.Logging.Dto;

namespace SimbioMed.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
