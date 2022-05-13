using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Microsoft.EntityFrameworkCore;
using SimbioMed.Authorization;
using SimbioMed.Store.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Store {




    public class StoreAppService : SimbioMedAppServiceBase, IStoreAppService {

        private readonly IRepository<Store> _storeRepository;

        public StoreAppService(IRepository<Store> storeRepository) {
            _storeRepository = storeRepository;
        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_Store_EditStore)]

        public async Task CreateStore(CreateStoreInput input) {
            var store = ObjectMapper.Map<Store>(input);
            await _storeRepository.InsertAsync(store);
        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_Store_EditStore)]

        public async Task DeleteStore(EntityDto input) {
            await _storeRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Store)]
        [AbpAuthorize(AppPermissions.Pages_Tenant_City)]

        public ListResultDto<StoreListDto> GetStore(GetStoreInput input) {
            var store = _storeRepository
                            .GetAll()
                            .Include(p=>p.City)
                            .WhereIf(
                                !input.Filter.IsNullOrEmpty(),
                                p => p.StoreName.ToLower().Contains(input.Filter.ToLower()) ||
                                     p.Address.ToLower().Contains(input.Filter.ToLower())

                            )
                            .OrderBy(p => p.StoreName)
                            .ToList();

            return new ListResultDto<StoreListDto>(ObjectMapper.Map<List<StoreListDto>>(store));
        }


        [AbpAuthorize(AppPermissions.Pages_Tenant_Store_EditStore)]

        public async Task EditStore(EditStoreInput input) {
            var city = await _storeRepository.GetAsync(input.Id);
            city.StoreName = input.StoreName;
            city.Address = input.Address;
            city.CityId = input.CityId;

            await _storeRepository.UpdateAsync(city);
        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_Store_EditStore)]

        public async Task<GetStoreForEditOutput> GetStoreForEdit(GetStoreForEditInput input) {
            var city = await _storeRepository.GetAsync(input.Id);
            return ObjectMapper.Map<GetStoreForEditOutput>(city);
        }
    }

}