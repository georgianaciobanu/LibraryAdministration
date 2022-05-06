using System;
using System.Collections.Generic;
using System.Text;

namespace SimbioMed.BookUnit.DtoDiscountBook {
    public class CreateDiscountBookInput {

        public virtual int? DiscountId { get; set; }
        public virtual int? BookUnitId { get; set; }
    }
}
