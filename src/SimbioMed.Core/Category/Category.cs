using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Category {
    [Table("Category")]
    public class Category: FullAuditedEntity {

        public const int MaxNameLenght = 200;
        [Required]
        [MaxLength(MaxNameLenght)]
        public virtual string Name { get; set; }
        [Required]
        public virtual string Code { get; set; }
    }
}
