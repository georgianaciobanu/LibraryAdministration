using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Author {
    [Table("Author")]
    public class Author: FullAuditedEntity {

        [ForeignKey("CityId")]
        public virtual City.City City { get; set; }
        public virtual int? CityId { get; set; }

        public const int MaxNameLenght = 200;
        [Required]
        [MaxLength(MaxNameLenght)]
        public virtual string FirstName { get; set; }
        [Required]
        [MaxLength(MaxNameLenght)]
        public virtual string LastName { get; set; }
        [Required]
        public virtual DateTime DateOfBirth { get; set; }
        public virtual DateTime DateOfDeath { get; set; }

        [Required]
        public virtual Gender Gender  { get; set; }

        public Guid? PhotoId { get; set; }
        public string PhotoFileBytes { get; set; }

      
    }


  
}
