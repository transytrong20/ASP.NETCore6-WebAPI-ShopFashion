using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Webapp.Application.RequestObjects;
using Shop.Webapp.Application.Services.Abstracts;
using Shop.Webapp.Application.Services.Implements;
using Shop.Webapp.Shared.ConstsDatas;

namespace Shop.WebApp.Web.Controllers
{
    //[Authorize]
    [ApiController]
    [Route($"{ApplicationConsts.FirstRoute}/cart")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add-to-cart")]
        public IActionResult AddToCart([FromForm] CreateCartModel model)
        {
            var result = _cartService.AddToCartAsync(model);
            return Ok(result);
        }

        [HttpGet("cart")]
        public IActionResult AddToCart(Guid Id)
        {
            var result = _cartService.CartAsync(Id);
            return Ok(result);
        }
    }
}
