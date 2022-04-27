using System.Threading.Tasks;
using Abp.Application.Services;
using SimbioMed.MultiTenancy.Payments.PayPal.Dto;

namespace SimbioMed.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalOrderId);

        PayPalConfigurationDto GetConfiguration();
    }
}
