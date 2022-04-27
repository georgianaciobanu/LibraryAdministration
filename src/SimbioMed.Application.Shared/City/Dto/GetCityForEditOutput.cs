using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimbioMed.City.Dto {
    public class GetCityForEditOutput : EntityDto {

        public virtual string CityName { get; set; }
        public virtual string Country { get; set; }
    }
}
