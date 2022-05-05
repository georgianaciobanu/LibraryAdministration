using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SimbioMed.BookUnit.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.BookUnit {
   public interface IBookUnitAppService : IApplicationService {

        ListResultDto<BookUnitListDto> GetBookUnit(GetBookUnitInput input);

        Task CreateBookUnit(CreateBookUnitInput input);

        Task DeleteBookUnit(EntityDto input);

       // Task<GetPictureOutput> GetPictureById(Guid pictureId);
    }
}
