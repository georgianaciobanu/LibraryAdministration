using System.Threading.Tasks;
using Abp.Webhooks;

namespace SimbioMed.WebHooks
{
    public interface IWebhookEventAppService
    {
        Task<WebhookEvent> Get(string id);
    }
}
