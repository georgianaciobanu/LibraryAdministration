using SimbioMed.BookUnit.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimbioMed.Sale.DtoSaleDetail {
    public class SaleDetailListDto {

        public int Id { get; set; }
        public virtual BookUnitListDto BookUnit { get; set; }
        public virtual int? BookUnitId { get; set; }
        public virtual int? SaleId { get; set; }

        public virtual int Qtty { get; set; }

        public virtual double PriceWithDiscount { get; set; }



    }
}
