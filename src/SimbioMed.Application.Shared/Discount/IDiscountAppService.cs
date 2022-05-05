using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SimbioMed.Discount.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Discount {
  public  interface IDiscountAppService:IApplicationService {
        ListResultDto<DiscountListDto> GetDiscounts(GetDiscountInput input);
        Task CreateDiscount(CreateDiscountInput input);

        Task DeleteDiscount(EntityDto input);



    }
}
