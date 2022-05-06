using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Sale {

    [Table("DiscountSale")]

    public class DiscountSale: FullAuditedEntity {

        [ForeignKey("DiscountId")]
        public virtual int? DiscountId { get; set; }
        public virtual Discount.Discount Discount { get; set; }
        
        [ForeignKey("SaleId")]
        public virtual int? SaleId { get; set; }
        public virtual Sale Sale { get; set; }


    }
}
