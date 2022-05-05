using Abp.Application.Services.Dto;
using SimbioMed.Book.DtoBookCategory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SimbioMed.Book.Dto {
   public class GetBookForEditOutput: EntityDto {


        public string Title { get; set; }
        public int AuthorId { get; set; }
        public Collection<BookCategoryListDto> Categories { get; set; }


    }
}
