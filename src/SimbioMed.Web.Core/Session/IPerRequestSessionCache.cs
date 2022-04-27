using System.Threading.Tasks;
using SimbioMed.Sessions.Dto;

namespace SimbioMed.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
