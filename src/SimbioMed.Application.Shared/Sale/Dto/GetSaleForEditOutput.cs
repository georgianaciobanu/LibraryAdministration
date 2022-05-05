using Abp.Application.Services.Dto;
using SimbioMed.Sale.DtoSaleDetail;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SimbioMed.Sale.Dto {
    public class GetSaleForEditOutput : EntityDto {
        public virtual string SaleNumber { get; set; }
        public virtual DateTime SaleDate { get; set; }
        public virtual int TotalQtty { get; set; }
        public virtual float TotalAmount { get; set; }

        public virtual int StoreId { get; set; }
        public virtual int CustomerId { get; set; }

        public Collection<SaleDetailListDto> Details { get; set; }

    }
}
