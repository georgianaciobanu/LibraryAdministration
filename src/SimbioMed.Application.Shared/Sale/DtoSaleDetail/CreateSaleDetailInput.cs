using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimbioMed.Sale.DtoSaleDetail {
    public class CreateSaleDetailInput {


        public virtual int? BookUnitId { get; set; }
        public virtual int? SaleId { get; set; }

        public virtual int Qtty { get; set; }
        public virtual double PriceWithDiscount { get; set; }

    }
}
