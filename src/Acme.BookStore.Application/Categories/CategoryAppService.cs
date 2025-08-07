using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
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
            _logger.LogInformation("Creating a new category with name: {Name}", input.Name);
            var category = ObjectMapper.Map<CreateCategoryDto, Category>(input);
            var result = await _categoryRepository.InsertAsync(category);
            
            _logger.LogInformation("Category created successfully with ID: {Id}", result.Id);
            return ObjectMapper.Map<Category, CategoryDto>(result);
        }

        public async Task DeleteAsync(Guid id)
        {
            _logger.LogInformation("Deleting category with ID: {Id}", id);
            var category = await _categoryRepository.GetAsync(id);
            if (category == null)
            {
                _logger.LogWarning("Category with ID: {Id} not found", id);
                throw new Exception($"Category with ID: {id} not found.");
            }
            await _categoryRepository.DeleteAsync(category);
            _logger.LogInformation("Category with ID: {Id} deleted successfully", id);
        }

        public async Task<CategoryDto> GetAsync(Guid id)
        {
            _logger.LogInformation("Getting category with ID: {Id}", id);

            Category? category = await _categoryRepository.GetAsync(id);

            return ObjectMapper.Map<Category, CategoryDto>(category);
        }

        public async Task<PagedResultDto<CategoryDto>> GetListAsync(CategoryGetListDto input)
        {
            _logger.LogInformation("Getting category list with filter: {Filter}, sorting: {Sorting}", input.Filter, input.Sorting);

            // Lấy tất cả categories có áp dụng bộ lọc nếu có
            var queryable = await _categoryRepository.GetQueryableAsync();
            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                queryable = queryable.Where(c => c.Name.Contains(input.Filter));
            }

            // Tổng số bản ghi sau lọc
            var totalCount = await AsyncExecuter.CountAsync(queryable);

            // Sắp xếp nếu có
            if (!string.IsNullOrWhiteSpace(input.Sorting))
            {
                queryable = queryable.OrderBy(input.Sorting);
            }
            else
            {
                //queryable = queryable.OrderBy(c => c.Name); // mặc định sắp xếp theo tên
                queryable = queryable.OrderByDescending(c => c.CreationTime); // mặc định sắp xếp theo thời gian tạo
            }

            // Phân trang
            var items = await AsyncExecuter.ToListAsync(
                queryable
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount)
            );

            // Map sang DTO
            var itemDtos = ObjectMapper.Map<List<Category>, List<CategoryDto>>(items);

            return new PagedResultDto<CategoryDto>(
                totalCount,
                itemDtos
            );
        }

        public async Task<CategoryDto> UpdateAsync(Guid id, UpdateCategoryDto input)
        {
            _logger.LogInformation("Updating category with ID: {Id}", id);
            var category = await _categoryRepository.GetAsync(id);
            if (category == null)
            {
                _logger.LogWarning("Category with ID: {Id} not found", id);
                throw new Exception($"Category with ID: {id} not found.");
            }

            var updatedCategory = ObjectMapper.Map<UpdateCategoryDto, Category>(input, category);
            var result = await _categoryRepository.UpdateAsync(category);

            _logger.LogInformation("Category with ID: {Id} updated successfully", id);
            return ObjectMapper.Map<Category, CategoryDto>(result);
        }
    }
}
