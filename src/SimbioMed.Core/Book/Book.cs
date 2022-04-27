using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Book {
    [Table("Book")]
   public class Book: FullAuditedEntity {
        public const int MaxGeneralLength = 500;
        public const int MaxPagesNo = 100000;

        [Required]
        [MaxLength(MaxGeneralLength)]
        public virtual string Title { get; set; }
       
        [ForeignKey("AuthorId")]
        public virtual Author.Author Author { get; set; }
        public virtual int? AuthorId { get; set; }
        public virtual ICollection<BookCategory> Categories { get; set; }


    }
}
