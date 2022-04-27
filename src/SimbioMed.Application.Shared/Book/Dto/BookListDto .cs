using Abp.Application.Services.Dto;
using SimbioMed.Book.DtoBookCategory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SimbioMed.Book.Dto {
    public class BookListDto {

        public int Id { get; set; }
        public  string Title { get; set; }
        public AuthorInBookListDto Author { get; set; }
        public int AuthorId { get; set; }
        public List<BookCategoryListDto> Categories { get; set; }

    }


    public class AuthorInBookListDto : CreationAuditedEntityDto {
        public  string FirstName { get; set; }
        public  string LastName { get; set; }
    }

   
}
