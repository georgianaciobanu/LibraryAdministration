using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimbioMed.Store.Dto {
   public class EditStoreInput : EntityDto {

        public virtual string StoreName { get; set; }
        public virtual string Address { get; set; }

        [ForeignKey("CityId")]
        public int? CityId { get; set; }
    }
}
