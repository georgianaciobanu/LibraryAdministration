using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Book {
    public class BookCategory: FullAuditedEntity {

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
        public virtual int? BookId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category.Category Category { get; set; }
        public virtual int? CategoryId { get; set; }
    }
}
