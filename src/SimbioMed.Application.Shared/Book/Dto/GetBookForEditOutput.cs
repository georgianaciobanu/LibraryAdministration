using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimbioMed.Book.Dto {
   public class GetBookForEditOutput: EntityDto {


        public string Title { get; set; }
        public int AuthorId { get; set; }


    }
}
