using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Discount {
    [Table("Discount")]

    public class Discount : FullAuditedEntity {
        public const int MaxValue = 100;

        [Required]
        public virtual string Description { get; set; }
        [Required]
        [MaxLength(MaxValue)]
        public virtual float Value { get; set; }

    }
}
