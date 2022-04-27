using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimbioMed.Category.Dto {
     public class GetCategoryForEditOutput: EntityDto {

        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
    }
}
