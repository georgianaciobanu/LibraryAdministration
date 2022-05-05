using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using SimbioMed.Discount.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Discount {
    public class DiscountAppService : SimbioMedAppServiceBase, IDiscountAppService {

        private readonly IRepository<Discount> _discountRepository;

        public DiscountAppService(IRepository<Discount> discountRepository) {
            _discountRepository = discountRepository;
        }
        public async Task CreateDiscount(CreateDiscountInput input) {
            var discount = ObjectMapper.Map<Discount>(input);
            await _discountRepository.InsertAsync(discount);
        }

        public async Task DeleteDiscount(EntityDto input) {
            await _discountRepository.DeleteAsync(input.Id);
        }

        public async Task EditDiscount(EditDiscountInput input) {
            var discount = await _discountRepository.GetAsync(input.Id);
            discount.Description = input.Description;
            discount.Value = input.Value;

            await _discountRepository.UpdateAsync(discount);
        }

        public async Task<GetDiscountForEditOutput> GetDiscountForEdit(GetDiscountForEditInput input) {
            var discount = await _discountRepository.GetAsync(input.Id);
            return ObjectMapper.Map<GetDiscountForEditOutput>(discount);
        }
        public ListResultDto<DiscountListDto> GetDiscounts(GetDiscountInput input) {
            var discounts = _discountRepository
                             .GetAll()
                             .WhereIf(
                                 !input.Filter.IsNullOrEmpty(),
                                 p => p.Description.ToLower().Contains(input.Filter.ToLower()) 

                             )
                             .OrderBy(p => p.Description)
                             .ToList();

            return new ListResultDto<DiscountListDto>(ObjectMapper.Map<List<DiscountListDto>>(discounts));
        }
    }
}
