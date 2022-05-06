using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SimbioMed.BookUnit.Dto;
using SimbioMed.BookUnit.DtoDiscountBook;
using SimbioMed.Sale.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.BookUnit {
   public interface IBookUnitAppService : IApplicationService {

        ListResultDto<BookUnitListDto> GetBookUnit(GetBookUnitInput input);

        Task<int> CreateBookUnit(CreateBookUnitInput input);

        Task DeleteBookUnit(GetBookInputForEditInput input);



        // Task<GetPictureOutput> GetPictureById(Guid pictureId);

        Collection<DiscountBookListDto> GetDiscountBook(GetBookUnitInput input);

        Task CreateDiscountBook(CreateDiscountBookInput input);
        Task DeleteDiscountBook(int input);
    }
}
