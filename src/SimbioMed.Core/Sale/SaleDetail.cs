using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Sale {

    [Table("SaleDetail")]
    public class SaleDetail : FullAuditedEntity{

        [ForeignKey("BookUnitId")]
        public virtual BookUnit.BookUnit BookUnit { get; set; }
        public virtual int? BookUnitId { get; set; }
        [ForeignKey("SaleId")]
        public virtual int? SaleId { get; set; }

        public virtual int Qtty { get; set; }
    }
}
