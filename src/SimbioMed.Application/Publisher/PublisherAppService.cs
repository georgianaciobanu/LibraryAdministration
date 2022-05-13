using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Microsoft.EntityFrameworkCore;
using SimbioMed.Authorization;
using SimbioMed.Publisher.Dto;
using SimbioMed.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Publisher {


    public class PublisherAppService : SimbioMedAppServiceBase, IPublisherAppService {

        private readonly IRepository<Publisher> _publisherRepository;
        private readonly IBinaryObjectManager _binaryObjectManager;


        public PublisherAppService(IRepository<Publisher> publisherRepository, IBinaryObjectManager binaryObjectManager) {
            _publisherRepository = publisherRepository;
            _binaryObjectManager = binaryObjectManager;

        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_Publisher_EditPublisher)]

        public async Task CreatePublisher(CreatePublisherInput input) {
            var publisher = ObjectMapper.Map<Publisher>(input);

            if (input.PhotoId != null) {
                var x = input.PhotoFileBytes.Split("base64,");
                var bytesPhoto = Convert.FromBase64String(x[1]);
                var storedPhoto = new BinaryObject(1, bytesPhoto);
                await _binaryObjectManager.SaveAsync(storedPhoto);
                publisher.PhotoId = storedPhoto.Id;
            }
            await _publisherRepository.InsertAsync(publisher);
        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_Publisher_EditPublisher)]

        public async Task DeletePublisher(EntityDto input) {
            await _publisherRepository.DeleteAsync(input.Id);
        }

        //public async Task<GetPictureOutput> GetPictureById(Guid pictureId) {
        //    var file = await _binaryObjectManager.GetOrNullAsync(pictureId);
        //    if (file == null) {
        //        return new GetPictureOutput(string.Empty);
        //    }

        //    return new GetPictureOutput(Convert.ToBase64String(file.Bytes));
        //}

        [AbpAuthorize(AppPermissions.Pages_Tenant_Publisher)]
        [AbpAuthorize(AppPermissions.Pages_Tenant_City)]

        public ListResultDto<PublisherListDto> GetPublisher(GetPublisherInput input) {
            var publisher = _publisherRepository
                           .GetAll()
                           .Include(p => p.City)
                           .WhereIf(
                               !input.Filter.IsNullOrEmpty(),
                               p => p.Name.ToLower().Contains(input.Filter.ToLower())
                                 
                           )
                           .OrderBy(p => p.Name)
                           .ToList();

            return new ListResultDto<PublisherListDto>(ObjectMapper.Map<List<PublisherListDto>>(publisher));
        }
        [AbpAuthorize(AppPermissions.Pages_Tenant_Publisher_EditPublisher)]

        public async Task EditPublisher(EditPublisherInput input) {
            var publisher = await _publisherRepository.GetAsync(input.Id);


            if (input.PhotoId == Guid.Empty && input.PhotoFileBytes != null && publisher.PhotoId != null) {
                await _binaryObjectManager.DeleteAsync(publisher.PhotoId.Value);
                var x = input.PhotoFileBytes.Split("base64,");
                var bytes = Convert.FromBase64String(x[1]);
                var storedFile = new BinaryObject(1, bytes);
                await _binaryObjectManager.SaveAsync(storedFile);
                publisher.PhotoId = storedFile.Id;
            } else if (input.PhotoId == Guid.Empty && input.PhotoFileBytes != null && publisher.PhotoId == null) {
                var x = input.PhotoFileBytes.Split("base64,");
                var bytes = Convert.FromBase64String(x[1]);
                var storedFile = new BinaryObject(1, bytes);
                await _binaryObjectManager.SaveAsync(storedFile);
                publisher.PhotoId = storedFile.Id;
            }
            publisher.PhotoFileBytes = input.PhotoFileBytes;
            publisher.Name = input.Name;
            publisher.CityId = input.CityId;
            publisher.Phone = input.Phone;
            publisher.Email = input.Email;


            await _publisherRepository.UpdateAsync(publisher);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Publisher_EditPublisher)]

        public async Task<GetPublisherForEditOutput> GetPublisherForEdit(GetPublisherForEditInput input) {
            var publisher = await _publisherRepository.GetAsync(input.Id);
            return ObjectMapper.Map<GetPublisherForEditOutput>(publisher);
        }

    }
}
