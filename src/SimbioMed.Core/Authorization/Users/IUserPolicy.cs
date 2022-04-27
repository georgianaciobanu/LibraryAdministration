using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace SimbioMed.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
