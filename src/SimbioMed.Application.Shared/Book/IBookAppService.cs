using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SimbioMed.Book.Dto;
using SimbioMed.Book.DtoBookCategory;
using SimbioMed.Category.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Book {
    public interface IBookAppService:IApplicationService {

        ListResultDto<BookListDto> GetBooks(GetBooksInput input);
        Collection<BookCategoryListDto> GetBookCategory(GetBooksInput input);
        Task<int> CreateBook(CreateBookInput input);
        Task CreateBookCategory(CreateBookCategoryInput input);
        Task DeleteBookCategory(int input);

        Task DeleteBook(GetBookForEditInput input);


    }
}
