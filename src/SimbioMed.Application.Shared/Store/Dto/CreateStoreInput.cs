using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SimbioMed.Store.Dto {
    public class CreateStoreInput {

        public virtual string StoreName { get; set; }
        [ForeignKey("CityId")]
        public int? CityId { get; set; }
        public virtual string Address { get; set; }
    }
}
