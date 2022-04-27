using System.Threading.Tasks;
using Abp.Application.Services;
using SimbioMed.MultiTenancy.Payments.Dto;
using SimbioMed.MultiTenancy.Payments.Stripe.Dto;

namespace SimbioMed.MultiTenancy.Payments.Stripe
{
    public interface IStripePaymentAppService : IApplicationService
    {
        Task ConfirmPayment(StripeConfirmPaymentInput input);

        StripeConfigurationDto GetConfiguration();

        Task<SubscriptionPaymentDto> GetPaymentAsync(StripeGetPaymentInput input);

        Task<string> CreatePaymentSession(StripeCreatePaymentSessionInput input);
    }
}