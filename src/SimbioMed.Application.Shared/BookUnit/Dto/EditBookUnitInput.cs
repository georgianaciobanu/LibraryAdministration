using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimbioMed.BookUnit.Dto {
   public class EditBookUnitInput: EntityDto {

        public virtual string ISBN { get; set; }
        public virtual int CopyNo { get; set; }
        public virtual float Price { get; set; }
        public virtual int PublishYear { get; set; }
        public virtual int Pages { get; set; }
        public virtual ConditionDto Condition { get; set; }

        //location
        public virtual string Unit { get; set; }//A-H
        public virtual int Column { get; set; } //1-20
        public virtual int Row { get; set; } //1-50

        public Guid? PhotoId { get; set; }
        public string PhotoFileBytes { get; set; }
        public virtual int? BookId { get; set; }
        public virtual int? PublisherId { get; set; }
        public virtual int? StoreId { get; set; }
    }
}
