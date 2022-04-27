using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Microsoft.EntityFrameworkCore;
using SimbioMed.Author.Dto;
using SimbioMed.Authorization.Users.Profile.Dto;
using SimbioMed.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Author {
    public class AuthorAppService : SimbioMedAppServiceBase, IAuthorAppService {

        private readonly IRepository<Author> _authorRepository;
        private readonly IBinaryObjectManager _binaryObjectManager;

        public AuthorAppService(IRepository<Author> authorRepository, IBinaryObjectManager binaryObjectManager) {
            _authorRepository = authorRepository;
            _binaryObjectManager = binaryObjectManager;

        }

        public async Task CreateAuthor(CreateAuthorInput input) {
            
            var author = ObjectMapper.Map<Author>(input);

            if (input.PhotoId != null) {
                var x = input.PhotoFileBytes.Split("base64,");
                var bytesPhoto = Convert.FromBase64String(x[1]);
                var storedPhoto = new BinaryObject(1, bytesPhoto);
                await _binaryObjectManager.SaveAsync(storedPhoto);
                author.PhotoId = storedPhoto.Id;
            }
            await _authorRepository.InsertAsync(author);
        }

        public async Task DeleteAuthor(EntityDto input) {
            await _authorRepository.DeleteAsync(input.Id);
        }

        public ListResultDto<AuthorListDto> GetAuthor(GetAuthorInput input) {
            var author = _authorRepository
                           .GetAll()
                           .Include(p => p.City)
                           .WhereIf(
                               !input.Filter.IsNullOrEmpty(),
                               p => p.FirstName.ToLower().Contains(input.Filter.ToLower()) ||
                                    p.LastName.ToLower().Contains(input.Filter.ToLower())

                           )
                           .OrderBy(p => p.LastName)
                           .ToList();

            return new ListResultDto<AuthorListDto>(ObjectMapper.Map<List<AuthorListDto>>(author));
        }


        public async Task EditAuthor(EditAuthorInput input) {
            var author = await _authorRepository.GetAsync(input.Id);


            if (input.PhotoId == Guid.Empty && input.PhotoFileBytes != null && author.PhotoId != null) {
                await _binaryObjectManager.DeleteAsync(author.PhotoId.Value);
                var x = input.PhotoFileBytes.Split("base64,");
                var bytes = Convert.FromBase64String(x[1]);
                var storedFile = new BinaryObject(1, bytes);
                await _binaryObjectManager.SaveAsync(storedFile);
                author.PhotoId = storedFile.Id;
            } else if (input.PhotoId == Guid.Empty && input.PhotoFileBytes != null && author.PhotoId == null) {
                var x = input.PhotoFileBytes.Split("base64,");
                var bytes = Convert.FromBase64String(x[1]);
                var storedFile = new BinaryObject(1, bytes);
                await _binaryObjectManager.SaveAsync(storedFile);
                author.PhotoId = storedFile.Id;
            }
            author.PhotoFileBytes = input.PhotoFileBytes;
            author.FirstName = input.FirstName;
            author.LastName = input.LastName;
            author.CityId = input.CityId;
            author.DateOfBirth = input.DateOfBirth;
            author.DateOfDeath = input.DateOfDeath;
            author.Gender = (Gender)input.Gender;


            await _authorRepository.UpdateAsync(author);
        }

        public async Task<GetAuthorForEditOutput> GetAuthorForEdit(GetAuthorForEditInput input) {
            var author = await _authorRepository.GetAsync(input.Id);
            return ObjectMapper.Map<GetAuthorForEditOutput>(author);
        }


        public async Task<GetPictureOutput> GetPictureById(Guid pictureId) {

            var file = await _binaryObjectManager.GetOrNullAsync(pictureId);
            if (file == null) {
                return new GetPictureOutput(string.Empty);
            }

            return new GetPictureOutput(Convert.ToBase64String(file.Bytes));

        }

        
    }
}
