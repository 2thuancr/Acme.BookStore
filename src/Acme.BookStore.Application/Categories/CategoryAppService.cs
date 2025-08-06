using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Categories
{
    public class CategoryAppService : ApplicationService, ICategoryAppService
    {
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly ILogger<CategoryAppService> _logger;

        public CategoryAppService(
            IRepository<Category, Guid> categoryRepository,
            ILogger<CategoryAppService> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto input)
        {
            throw new NotImplementedException("CreateAsync method is not implemented yet.");
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryDto> GetAsync(Guid id)
        {
            _logger.LogInformation("Getting category with ID: {Id}", id);

            Category? category = await _categoryRepository.GetAsync(id);

            return ObjectMapper.Map<Category, CategoryDto>(category);
        }

        public Task<PagedResultDto<CategoryDto>> GetListAsync(CategoryGetListDto input)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDto> UpdateAsync(Guid id, UpdateCategoryDto input)
        {
            throw new NotImplementedException();
        }
    }
}
