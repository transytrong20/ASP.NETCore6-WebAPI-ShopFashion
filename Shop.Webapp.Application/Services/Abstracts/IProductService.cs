using Shop.Webapp.Application.Dto;
using Shop.Webapp.Application.RequestObjects;
using Shop.Webapp.Shared.ApiModels.Results;

namespace Shop.Webapp.Application.Services.Abstracts
{
    public interface IProductService
    {
        Task<ProductDto> CreateAsync(CreateOrUpdateProductModel model);
        Task<ProductDto> GetByIdAsync(Guid id);
        Task RemoveAsync(Guid id);
        Task<ProductDto> UpdateAsync(Guid id, CreateOrUpdateProductModel model);
        Task<GenericPagingResult<ProductDto>> GetPagingAsync(OrderInformationPagingFilter filter);
    }
}
