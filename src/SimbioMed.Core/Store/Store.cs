using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Store {

    [Table("Store")]
    public class Store: FullAuditedEntity {

        [ForeignKey("CityId")]
        public virtual City.City City { get; set; }
        public virtual int? CityId { get; set; }

        public const int MaxNameLenght = 200;
        [Required]
        [MaxLength(MaxNameLenght)]
        public virtual string StoreName { get; set; }
        [Required]
        [MaxLength(MaxNameLenght)]
        public virtual string Address { get; set; }


    }
}
