using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Publisher {
    [Table("Publisher")]
    public class Publisher: FullAuditedEntity {

        [ForeignKey("CityId")]
        public virtual City.City City { get; set; }
        public virtual int? CityId { get; set; }

        public const int MaxNameLenght = 200;
        [Required]
        [MaxLength(MaxNameLenght)]
        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual string Phone { get; set; }

        public Guid? PhotoId { get; set; }
        public string PhotoFileBytes { get; set; }
    }
}
