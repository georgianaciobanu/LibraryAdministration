using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SimbioMed.Category.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimbioMed.Category {
    public interface ICategoryAppService: IApplicationService {

        ListResultDto<CategoryListDto> GetCategories(GetCategoriesInput input);

        Task CreateCategory(CreateCategoryInput input);

        Task DeleteCategory(EntityDto input);
    }
}
