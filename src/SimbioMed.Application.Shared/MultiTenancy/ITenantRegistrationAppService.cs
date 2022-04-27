using System.Threading.Tasks;
using Abp.Application.Services;
using SimbioMed.Editions.Dto;
using SimbioMed.MultiTenancy.Dto;

namespace SimbioMed.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}