using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Sale {
    [Table("Sale")]

    public class Sale : FullAuditedEntity {
        
       
        public virtual string SaleNumber { get; set; }
        public virtual DateTime SaleDate { get; set; }
        public virtual int TotalQtty { get; set; }
        public virtual double TotalAmount { get; set; }    

        [ForeignKey("StoreId")]
        public virtual Store.Store Store { get; set; }
        public virtual int? StoreId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer.Customer Customer { get; set; }
        public virtual int? CustomerId { get; set; }

        public virtual ICollection<SaleDetail> Details { get; set; }

    }
}
