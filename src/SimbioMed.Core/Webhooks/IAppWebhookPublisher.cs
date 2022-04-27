using System.Threading.Tasks;
using SimbioMed.Authorization.Users;

namespace SimbioMed.WebHooks
{
    public interface IAppWebhookPublisher
    {
        Task PublishTestWebhook();
    }
}
