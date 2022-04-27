using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SimbioMed.Publisher.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Publisher {
   public interface IPublisherAppService : IApplicationService {

        ListResultDto<PublisherListDto> GetPublisher(GetPublisherInput input);

        Task CreatePublisher(CreatePublisherInput input);

        Task DeletePublisher(EntityDto input);

        //Task<GetPictureOutput> GetPictureById(Guid pictureId);

    }
}
