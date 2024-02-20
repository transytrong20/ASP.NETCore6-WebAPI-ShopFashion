using Shop.Webapp.Application.Dto;
using Shop.Webapp.Application.RequestObjects;

namespace Shop.Webapp.Application.Services.Abstracts
{
    public interface ICartService
    {
        Task<CartDto> AddToCartAsync(CreateCartModel model);
        Task<CartDto> CartAsync(Guid Id);
    }
}
