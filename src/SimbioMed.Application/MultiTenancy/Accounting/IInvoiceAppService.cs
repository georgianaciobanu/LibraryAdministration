using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using SimbioMed.MultiTenancy.Accounting.Dto;

namespace SimbioMed.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
