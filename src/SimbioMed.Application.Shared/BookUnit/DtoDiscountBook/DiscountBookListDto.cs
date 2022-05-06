using SimbioMed.Discount.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimbioMed.BookUnit.DtoDiscountBook {
   public class DiscountBookListDto {

        public int Id { get; set; }
        public virtual DiscountListDto Discount { get; set; }
        public virtual int? DiscountId { get; set; }
        public virtual int? BookUnitId { get; set; }
    }
}
