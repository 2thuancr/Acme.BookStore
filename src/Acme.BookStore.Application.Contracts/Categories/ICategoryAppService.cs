using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Categories
{
    public interface ICategoryAppService : IApplicationService
    {
        /// <summary>
        /// Gets a paginated list of categories based on the input criteria.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<CategoryDto>> GetListAsync(CategoryGetListDto input);

        Task<CategoryDto> GetAsync(Guid id);

        Task<CategoryDto> CreateAsync(CreateCategoryDto input);

        Task<CategoryDto> UpdateAsync(Guid id, UpdateCategoryDto input);

        Task DeleteAsync(Guid id);
    }
}
