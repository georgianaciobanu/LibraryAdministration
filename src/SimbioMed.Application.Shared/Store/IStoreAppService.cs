using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SimbioMed.Store.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Store {
    public interface IStoreAppService : IApplicationService {

        ListResultDto<StoreListDto> GetStore(GetStoreInput input);

        Task CreateStore(CreateStoreInput input);

        Task DeleteStore(EntityDto input);

    }
}
