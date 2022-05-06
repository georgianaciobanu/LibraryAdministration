using SimbioMed.Discount.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimbioMed.Sale.DtoDiscountSale {
   public class DiscountSaleListDto {

        public int Id { get; set; }
        public virtual DiscountListDto Discount { get; set; }
        public virtual int? DiscountId { get; set; }
        public virtual int? SaleId { get; set; }
    }
}
