﻿using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Microsoft.EntityFrameworkCore;
using SimbioMed.Authorization;
using SimbioMed.Book.Dto;
using SimbioMed.Book.DtoBookCategory;
using SimbioMed.Category;
using SimbioMed.Category.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Book {
    [AbpAuthorize(AppPermissions.Pages_Tenant_Book)]
    [AbpAuthorize(AppPermissions.Pages_Tenant_Book_CreateBook)]
    public class BookAppService : SimbioMedAppServiceBase, IBookAppService {
        private readonly IRepository<Book> _bookRepository;
        public readonly IRepository<Author.Author> _authorRepository;
         public readonly IRepository<BookCategory> _bookCategoriesRepository;




        public BookAppService(IRepository<Book> bookRepository, IRepository<BookCategory> bookCategoriesRepository) {
            _bookRepository = bookRepository;
            _bookCategoriesRepository = bookCategoriesRepository;

        }

        public async Task<int> CreateBook(CreateBookInput input) {
            var book = ObjectMapper.Map<Book>(input);
           // await _bookRepository.InsertAsync(book);
            var entityId = await _bookRepository.InsertAndGetIdAsync(book);

            return entityId;
        }

        public async Task CreateBookCategory(CreateBookCategoryInput input) {
            var bookCategory = ObjectMapper.Map<BookCategory>(input);
            await _bookCategoriesRepository.InsertAsync(bookCategory);
        }

        public async Task DeleteBook(EntityDto input) {
            await _bookRepository.DeleteAsync(input.Id);
        }

        public async Task DeleteBookCategory(EntityDto input) {
            await _bookCategoriesRepository.DeleteAsync(input.Id);
        }


        public async Task EditBook(EditBookInput input) {
            var book = await _bookRepository.GetAsync(input.Id);
            book.Title = input.Title;
            book.AuthorId = input.AuthorId;
            await _bookRepository.UpdateAsync(book);
        }

        public async Task<GetBookForEditOutput> GetBookForEdit(GetBookForEditInput input) {
            var book = await _bookRepository.GetAsync(input.Id);
            return ObjectMapper.Map<GetBookForEditOutput>(book);
        }
        public  ListResultDto<BookListDto> GetBooks(GetBooksInput input) {

            var books =   _bookRepository
                .GetAll()
                .Include(p => p.Author)  
                //.Include(p => p.Categories)
                .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p => p.Title.ToLower().Contains(input.Filter.ToLower())
                )
                .OrderBy(p => p.Title)
                .ToList();



            return new ListResultDto<BookListDto>(ObjectMapper.Map<List<BookListDto>>(books));
        }

        public async Task<ListResultDto<BookCategoryListDto>> GetBookCategory(GetBooksInput input) {
            var books =  _bookCategoriesRepository
                .GetAll()
                .Include(p => p.Book)
                .WhereIf(
                    int.TryParse(input.Filter, out var x),
                    p => p.Book.Id.Equals(int.Parse(input.Filter))
                )
                .ToList();

            return new ListResultDto<BookCategoryListDto>(ObjectMapper.Map<List<BookCategoryListDto>>(books));
        }


        public ListResultDto<BookListDto> GetBooksByAuthor(GetBooksInput input) {
            var books = _bookRepository
                .GetAll()
                .Include(p => p.Author)
                .WhereIf(
                    int.TryParse(input.Filter, out var x),
                    p => p.Author.Id.Equals(int.Parse(input.Filter))
                )
                .OrderBy(p => p.Title)
                .ToList();

            return new ListResultDto<BookListDto>(ObjectMapper.Map<List<BookListDto>>(books));
        }

      
    }
}