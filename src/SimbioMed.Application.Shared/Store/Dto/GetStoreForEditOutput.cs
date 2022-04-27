using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimbioMed.Store.Dto {
    public class GetStoreForEditOutput : EntityDto {

        public virtual string StoreName { get; set; }
        public virtual string Address { get; set; }
        public int? CityId { get; set; }
    }
}
