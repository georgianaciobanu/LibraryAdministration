﻿using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Microsoft.EntityFrameworkCore;
using SimbioMed.BookUnit.Dto;
using SimbioMed.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.BookUnit {
    public class BookUnitAppService : SimbioMedAppServiceBase, IBookUnitAppService {

        private readonly IRepository<BookUnit> _bookUnitRepository;
        private readonly IBinaryObjectManager _binaryObjectManager;


        public BookUnitAppService(IRepository<BookUnit> bookUnitRepository, IBinaryObjectManager binaryObjectManager) {
            _bookUnitRepository = bookUnitRepository;
            _binaryObjectManager = binaryObjectManager;

        }
        public async Task CreateBookUnit(CreateBookUnitInput input) {
            var book = ObjectMapper.Map<BookUnit>(input);

            if (input.PhotoId != null) {
                var x = input.PhotoFileBytes.Split("base64,");
                var bytesPhoto = Convert.FromBase64String(x[1]);
                var storedPhoto = new BinaryObject(1, bytesPhoto);
                await _binaryObjectManager.SaveAsync(storedPhoto);
                book.PhotoId = storedPhoto.Id;
            }
            await _bookUnitRepository.InsertAsync(book);
        }

        public async Task DeleteBookUnit(EntityDto input) {
            await _bookUnitRepository.DeleteAsync(input.Id);
        }

        public ListResultDto<BookUnitListDto> GetBookUnit(GetBookUnitInput input) {
            var book = _bookUnitRepository
                            .GetAll()
                            .Include(p => p.Publisher)
                            .Include(p => p.Book)
                            .Include(p => p.Book.Author)
                            .Include(p => p.Store)
                            .WhereIf(
                                !input.Filter.IsNullOrEmpty(),
                                p => p.ISBN.ToLower().Contains(input.Filter.ToLower()) ||
                                     p.Book.Title.ToLower().Contains(input.Filter.ToLower())||
                                     p.Book.Author.FirstName.ToLower().Contains(input.Filter.ToLower()) ||
                                     p.Book.Author.LastName.ToLower().Contains(input.Filter.ToLower()) ||
                                     p.Book.Author.LastName.ToLower().Contains(input.Filter.ToLower()) ||
                                     p.Publisher.Name.ToLower().Contains(input.Filter.ToLower())

                            )
                            .OrderBy(p => p.Book.Title)
                            .ToList();

            return new ListResultDto<BookUnitListDto>(ObjectMapper.Map<List<BookUnitListDto>>(book));
        }

        public async Task<GetBookInputForEditOutput> GetBookUnitForEdit(GetBookInputForEditInput input) {
            var book = await _bookUnitRepository.GetAsync(input.Id);
            return ObjectMapper.Map<GetBookInputForEditOutput>(book);
        }

        //public async Task<GetPictureOutput> GetPictureById(Guid pictureId) {
        //    var file = await _binaryObjectManager.GetOrNullAsync(pictureId);
        //    if (file == null) {
        //        return new GetPictureOutput(string.Empty);
        //    }

        //    return new GetPictureOutput(Convert.ToBase64String(file.Bytes));

        //}



        public async Task EditBookUnit(EditBookUnitInput input) {
            var book = await _bookUnitRepository.GetAsync(input.Id);


            if (input.PhotoId == Guid.Empty && input.PhotoFileBytes != null && book.PhotoId != null) {
                await _binaryObjectManager.DeleteAsync(book.PhotoId.Value);
                var x = input.PhotoFileBytes.Split("base64,");
                var bytes = Convert.FromBase64String(x[1]);
                var storedFile = new BinaryObject(1, bytes);
                await _binaryObjectManager.SaveAsync(storedFile);
                book.PhotoId = storedFile.Id;
            } else if (input.PhotoId == Guid.Empty && input.PhotoFileBytes != null && book.PhotoId == null) {
                var x = input.PhotoFileBytes.Split("base64,");
                var bytes = Convert.FromBase64String(x[1]);
                var storedFile = new BinaryObject(1, bytes);
                await _binaryObjectManager.SaveAsync(storedFile);
                book.PhotoId = storedFile.Id;
            }
            book.PhotoFileBytes = input.PhotoFileBytes;
            book.Pages = input.Pages;
            book.CopyNo = input.CopyNo;
            book.ISBN = input.ISBN;
            book.PublishYear = input.PublishYear;
            book.Price = input.Price;
            book.Condition = (Condition)input.Condition;
            book.Unit = input.Unit;
            book.Column = input.Column;
            book.Row = input.Row;
            book.PublisherId = input.PublisherId;
            book.BookId = input.BookId;
            book.StoreId = input.StoreId;



            await _bookUnitRepository.UpdateAsync(book);
        }
    }
}
