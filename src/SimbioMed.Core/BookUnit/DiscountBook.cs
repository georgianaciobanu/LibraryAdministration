using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.BookUnit {

    [Table("DiscountBook")]

    public class DiscountBook: FullAuditedEntity {

        [ForeignKey("DiscountId")]
        public virtual int? DiscountId { get; set; }
        public virtual Discount.Discount Discount { get; set; }

        [ForeignKey("BookUnitId")]
        public virtual int? BookUnitId { get; set; }
        public virtual BookUnit BookUnit { get; set; }
    }
}
