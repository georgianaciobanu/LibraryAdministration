using Abp.AspNetCore.Mvc.Authorization;
using SimbioMed.Authorization;
using SimbioMed.Storage;
using Abp.BackgroundJobs;

namespace SimbioMed.Web.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : UsersControllerBase
    {
        public UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager)
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}