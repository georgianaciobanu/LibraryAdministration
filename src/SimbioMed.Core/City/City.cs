using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.City {
    [Table("Cities")]
    public class City: FullAuditedEntity {

        public const int MaxNameLenght = 200;
        [Required]
        [MaxLength(MaxNameLenght)]
        public virtual string CityName { get; set; }
        [Required]
        [MaxLength(MaxNameLenght)]
        public virtual string Country { get; set; }
    }
}
