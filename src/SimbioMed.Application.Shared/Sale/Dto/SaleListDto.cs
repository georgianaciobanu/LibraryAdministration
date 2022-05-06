using SimbioMed.Customer.Dto;
using SimbioMed.Sale.DtoDiscountSale;
using SimbioMed.Sale.DtoSaleDetail;
using SimbioMed.Store.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SimbioMed.Sale.Dto {
    public class SaleListDto {
        public virtual int Id { get; set; }
        public virtual string SaleNumber { get; set; }
        public virtual DateTime SaleDate { get; set; }
        public virtual int TotalQtty { get; set; }
        public virtual float TotalAmount { get; set; }

        public virtual StoreListDto Store { get; set; }
        public virtual int StoreId { get; set; }
        public virtual CustomerListDto Customer { get; set; }
        public virtual int CustomerId { get; set; }
        public Collection<SaleDetailListDto> Details { get; set; }
        public Collection<DiscountSaleListDto> Discounts { get; set; }

    }
}
