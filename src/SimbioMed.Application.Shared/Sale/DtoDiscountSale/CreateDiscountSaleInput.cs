using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimbioMed.Sale.DtoDiscountSale {
    public class CreateDiscountSaleInput {

        public virtual int? DiscountId { get; set; }
        public virtual int? SaleId { get; set; }
    }
}
