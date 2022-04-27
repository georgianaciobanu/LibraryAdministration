using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SimbioMed.Author.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Author {
   public  interface IAuthorAppService : IApplicationService {

        ListResultDto<AuthorListDto> GetAuthor(GetAuthorInput input);

        Task CreateAuthor(CreateAuthorInput input);

        Task DeleteAuthor(EntityDto input);

        Task<GetPictureOutput> GetPictureById(Guid pictureId);




    }
}
