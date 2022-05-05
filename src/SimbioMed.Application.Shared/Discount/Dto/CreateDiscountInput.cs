using System;
using System.Collections.Generic;
using System.Text;

namespace SimbioMed.Discount.Dto {
    public class CreateDiscountInput {
        public virtual string Description { get; set; }

        public virtual float Value { get; set; }
    }
}
