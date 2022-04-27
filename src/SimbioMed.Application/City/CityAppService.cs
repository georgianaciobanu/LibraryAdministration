using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using SimbioMed.Book.Dto;
using SimbioMed.City.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.City {
    public class CityAppService : SimbioMedAppServiceBase, ICityAppService {

        private readonly IRepository<City> _cityRepository;

        public CityAppService(IRepository<City> cityRepository) {
            _cityRepository = cityRepository;
        }
        public async Task CreateCity(CreateCityInput input) {
            var city = ObjectMapper.Map<City>(input);
            await _cityRepository.InsertAsync(city);
        }

        public async Task DeleteCity(EntityDto input) {
            await _cityRepository.DeleteAsync(input.Id);

        }


        public async Task EditCity(EditCityInput input) {
            var city = await _cityRepository.GetAsync(input.Id);
            city.CityName = input.CityName;
            city.Country = input.Country;

            await _cityRepository.UpdateAsync(city);
        }

        public async Task<GetCityForEditOutput> GetCityForEdit(GetCityForEditInput input) {
            var city = await _cityRepository.GetAsync(input.Id);
            return ObjectMapper.Map<GetCityForEditOutput>(city);
        }
        public ListResultDto<CityListDto> GetCity(GetCityInput input) {
            var city = _cityRepository
                            .GetAll()
                            .WhereIf(
                                !input.Filter.IsNullOrEmpty(),
                                p => p.CityName.ToLower().Contains(input.Filter.ToLower()) ||
                                     p.Country.ToLower().Contains(input.Filter.ToLower())

                            )
                            .OrderBy(p => p.CityName)
                            .ToList();

            return new ListResultDto<CityListDto>(ObjectMapper.Map<List<CityListDto>>(city));
        }

        
    }
}
