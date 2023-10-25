using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Webapp.Application.Dto;
using Shop.Webapp.Application.RequestObjects;
using Shop.Webapp.Application.Services.Abstracts;
using Shop.Webapp.Shared.ApiModels.Results;
using Shop.Webapp.Shared.ConstsDatas;

namespace Shop.WebApp.Web.Controllers
{
    //[Authorize]
    [ApiController]
    [Route($"{ApplicationConsts.FirstRoute}/category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("create")]
        public async Task<CategoryDto> CreateAsync([FromBody] CreateOrUpdateCategoryModel model)
        {
            return await _categoryService.CreateAsync(model);
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<CategoryDto[]> GetAllAsync()
        {
            return await _categoryService.GetAllAsync();
        }

        [HttpGet("paging")]
        public async Task<GenericPagingResult<CategoryDto>> GetPagingAsync([FromQuery] CategoryPagingFilter filter)
        {
            return await _categoryService.GetPagingAsync(filter);
        }

        [HttpDelete("{id}/remove")]
        public async Task<GenericActionResult> RemoveAsync(Guid id)
        {
            await _categoryService.RemoveAsync(id);
            return new GenericActionResult("Remove.Success");
        }

        [HttpPut("{id}/update")]
        public async Task<CategoryDto> UpdateAsync(Guid id, [FromBody] CreateOrUpdateCategoryModel model)
        {
            return await _categoryService.UpdateAsync(id, model);
        }

        [HttpPut("update-category-index")]
        public async Task<string> UpdateNewsIndexAsync([FromForm] Guid indexId, int index)
        {
            return await _categoryService.UpdateIndexAsync(indexId, index);
        }
    }
}
