using Microsoft.AspNetCore.Mvc;
using Shop.Webapp.Application.Dto;
using Shop.Webapp.Application.RequestObjects;
using Shop.Webapp.Application.Services.Abstracts;
using Shop.Webapp.Shared.ConstsDatas;

namespace Shop.WebApp.Web.Controllers
{
    [ApiController]
    [Route($"{ApplicationConsts.FirstRoute}/product")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create")]
        public async Task<ProductDto> CreateAsync([FromForm] CreateOrUpdateProductModel model)
        {
            return await _productService.CreateAsync(model);
        }
    }
}
