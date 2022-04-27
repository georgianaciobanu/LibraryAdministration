using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SimbioMed.Category.Dto;
using SimbioMed.City.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.City {
    public interface ICityAppService : IApplicationService {

        ListResultDto<CityListDto> GetCity(GetCityInput input);

        Task CreateCity(CreateCityInput input);

        Task DeleteCity(EntityDto input);
    }
}
