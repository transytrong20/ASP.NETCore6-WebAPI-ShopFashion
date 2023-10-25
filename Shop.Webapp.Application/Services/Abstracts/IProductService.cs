using Shop.Webapp.Application.Dto;
using Shop.Webapp.Application.RequestObjects;

namespace Shop.Webapp.Application.Services.Abstracts
{
    public interface IProductService
    {
        Task<ProductDto> CreateAsync(CreateOrUpdateProductModel model);
    }
}
