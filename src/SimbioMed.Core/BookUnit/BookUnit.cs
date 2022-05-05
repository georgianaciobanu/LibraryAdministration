using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.BookUnit {
    [Table("BookUnit")]          
   public class BookUnit : FullAuditedEntity {

        public const int MaxISBNLenght = 13;


        [Required]
        [MaxLength(MaxISBNLenght)]
        public virtual string ISBN { get; set; }
        public virtual int CopyNo { get; set; }
        public virtual float Price { get; set; }
        public virtual int PublishYear { get; set; }
        public virtual int Pages { get; set; }
        public virtual Condition Condition { get; set; }

        //location
        public virtual string Unit { get; set; }//A-H
        public virtual int Column { get; set; } //1-20
        public virtual int Row { get; set; } //1-50

        public Guid? PhotoId { get; set; }
        public string PhotoFileBytes { get; set; }

        [ForeignKey("BookId")]
        public virtual Book.Book Book { get; set; }
        public virtual int? BookId { get; set; }

        [ForeignKey("PublisherId")]
        public virtual Publisher.Publisher Publisher { get; set; }
        public virtual int? PublisherId { get; set; }

        [ForeignKey("StoreId")]
        public virtual Store.Store Store { get; set; }
        public virtual int? StoreId { get; set; }



    }
}
