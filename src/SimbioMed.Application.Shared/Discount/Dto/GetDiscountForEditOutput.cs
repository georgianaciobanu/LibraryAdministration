using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimbioMed.Discount.Dto {
   public class GetDiscountForEditOutput : EntityDto {
        public virtual string Description { get; set; }

        public virtual float Value { get; set; }
    }
}
