using System.Threading.Tasks;
using Abp.Application.Services;
using SimbioMed.Sessions.Dto;

namespace SimbioMed.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
