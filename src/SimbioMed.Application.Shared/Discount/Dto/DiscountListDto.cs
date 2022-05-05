using System;
using System.Collections.Generic;
using System.Text;

namespace SimbioMed.Discount.Dto {
    public class DiscountListDto {
        public int Id { get; set; }

        public virtual string Description { get; set; }

        public virtual float Value { get; set; }
    }
}
