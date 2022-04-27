using System.Threading.Tasks;
using Abp.Application.Services;

namespace SimbioMed.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}
