using Shop.Webapp.Application.Dto;
using Shop.Webapp.Application.RequestObjects;
using Shop.Webapp.Shared.ApiModels.Results;

namespace Shop.Webapp.Application.Services.Abstracts
{
    public interface ICategoryService
    {
        Task<CategoryDto> CreateAsync(CreateOrUpdateCategoryModel model);
        Task<CategoryDto[]> GetAllAsync();
        Task<GenericPagingResult<CategoryDto>> GetPagingAsync(CategoryPagingFilter filter);
        Task RemoveAsync(Guid id);
        Task<CategoryDto> UpdateAsync(Guid id, CreateOrUpdateCategoryModel model);
        Task<string> UpdateIndexAsync(Guid indexId, int index);
    }
}
