using Microsoft.AspNetCore.Mvc;
using Shop.Webapp.Application.Dto;
using Shop.Webapp.Application.RequestObjects;
using Shop.Webapp.Application.Services.Abstracts;
using Shop.Webapp.Shared.ApiModels.Results;
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

        [HttpGet("get/{id}")]
        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            return await _productService.GetByIdAsync(id);
        }

        [HttpDelete("{id}/remove")]
        public async Task<GenericActionResult> RemoveAsync(Guid id)
        {
            await _productService.RemoveAsync(id);
            return new GenericActionResult(ActionMessage.Remove);
        }

        [HttpPut("{id}/update")]
        public async Task<ProductDto> UpdateAsync(Guid id, [FromForm] CreateOrUpdateProductModel model)
        {
            return await _productService.UpdateAsync(id, model);
        }

        [HttpGet("paging")]
        public async Task<GenericPagingResult<ProductDto>> GetPagingAsync([FromQuery] OrderInformationPagingFilter filter)
        {
            return await _productService.GetPagingAsync(filter);
        }

        [HttpGet("getlistProduct")]
        public IActionResult GetListProduct()
        {
            var result = _productService.GetlistProduct();
            return Ok(result); 
        }

        [HttpGet("getlistProductSaleTurn")]
        public IActionResult GetListProductSaleTurn()
        {
            var result = _productService.GetlistProductSaleTurn();
            return Ok(result);
        }


    }
}
