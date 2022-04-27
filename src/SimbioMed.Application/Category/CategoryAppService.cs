using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using SimbioMed.Book.Dto;
using SimbioMed.Category.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Category {
    public class CategoryAppService : SimbioMedAppServiceBase, ICategoryAppService {

        private readonly IRepository<Category> _categoryRepository;

        public CategoryAppService(IRepository<Category> categoryRepository) {
            _categoryRepository = categoryRepository;
        }
        public async Task CreateCategory(CreateCategoryInput input) {
            var category = ObjectMapper.Map<Category>(input);
            await _categoryRepository.InsertAsync(category);
        }

        public async Task DeleteCategory(EntityDto input) {
            await _categoryRepository.DeleteAsync(input.Id);

        }


        public async Task EditCategory(EditCategoryInput input) {
            var category = await _categoryRepository.GetAsync(input.Id);
            category.Name = input.Name;
            category.Code = input.Code;
           
            await _categoryRepository.UpdateAsync(category);
        }

        public async Task<GetCategoryForEditOutput> GetCategoryForEdit(GetCategoryForEditInput input) {
            var category = await _categoryRepository.GetAsync(input.Id);
            return ObjectMapper.Map<GetCategoryForEditOutput>(category);
        }
        public  ListResultDto<CategoryListDto> GetCategories(GetCategoriesInput input) {
            var categories = _categoryRepository
                            .GetAll()
                            .WhereIf(
                                !input.Filter.IsNullOrEmpty(),
                                p => p.Name.ToLower().Contains(input.Filter.ToLower()) ||
                                     p.Code.ToLower().Contains(input.Filter.ToLower()) 
                                     
                            )
                            .OrderBy(p => p.Name)
                            .ToList();

            return new ListResultDto<CategoryListDto>(ObjectMapper.Map<List<CategoryListDto>>(categories));
        }
    }
}
