using Abp.Application.Services.Dto;
using SimbioMed.Book.Dto;
using SimbioMed.Category.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimbioMed.Book.DtoBookCategory {
   public class BookCategoryListDto {

        public int Id { get; set; }

       // public BookListDto Book { get; set; }
        public int BookId { get; set; }

        public CategoryFromBookListDto Category { get; set; }
        public int CategoryId { get; set; }

    }

    public class CategoryFromBookListDto : CreationAuditedEntityDto {
        public string Name { get; set; }

        public string Code { get; set; }
    }



}

