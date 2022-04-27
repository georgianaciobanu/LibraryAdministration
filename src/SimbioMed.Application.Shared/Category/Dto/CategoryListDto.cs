using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimbioMed.Category.Dto {
 
    public class CategoryListDto {

        public int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Code { get; set; }

    }
}
