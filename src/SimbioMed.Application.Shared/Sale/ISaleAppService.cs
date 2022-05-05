using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SimbioMed.Sale.Dto;
using SimbioMed.Sale.DtoSaleDetail;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Sale {
  public interface ISaleAppService : IApplicationService {

        ListResultDto<SaleListDto> GetSale(GetSaleInput input);
        Task<int> CreateSale(CreateSaleInput input);

        Collection<SaleDetailListDto> GetSaleDetail(GetSaleInput input);

        Task CreateSaleDetail(CreateSaleDetailInput input);
        Task DeleteSaleDetail(int input);

        Task DeleteSale(GetSaleForEditInput input);

    }
}
