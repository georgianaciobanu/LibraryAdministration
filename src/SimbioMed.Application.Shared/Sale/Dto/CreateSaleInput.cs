using System;
using System.Collections.Generic;
using System.Text;

namespace SimbioMed.Sale.Dto {
   public class CreateSaleInput {
        public virtual string SaleNumber { get; set; }
        public virtual DateTime SaleDate { get; set; }
        public virtual int TotalQtty { get; set; }
        public virtual float TotalAmount { get; set; }
        public virtual int StoreId { get; set; }
        public virtual int CustomerId { get; set; }
    }
}
