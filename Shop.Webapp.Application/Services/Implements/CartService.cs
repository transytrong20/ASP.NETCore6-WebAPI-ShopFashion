using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Webapp.Application.Dto;
using Shop.Webapp.Application.RequestObjects;
using Shop.Webapp.Application.Services.Abstracts;
using Shop.Webapp.Domain;
using Shop.Webapp.EFcore.Repositories.Abstracts;
using Shop.Webapp.EFcore.Repositories.Impls;
using Shop.Webapp.Shared.ApiModels;
using Shop.Webapp.Shared.ConstsDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Shop.Webapp.Application.Services.Implements
{
    public class CartService : AppService, ICartService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<User> _usertRepository;
        private readonly IRepository<CartProduct> _cartProductRepository;
        public CartService(
            IRepository<CartProduct> cartProductRepository,
            IRepository<Product> productRepository,
            IRepository<User> usertRepository,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            IMapper mapper) : base(unitOfWork, currentUser, mapper
            )
        {
            _productRepository = productRepository;
            _usertRepository = usertRepository;
            _cartProductRepository = cartProductRepository;
        }
        public async Task<CartDto> AddToCartAsync(CreateCartModel model)
        {
            if (!_productRepository.AsNoTracking().Any(_ => model.ProductId == _.Id))
                ThrowModelError(nameof(model.ProductId), MessageError.DataNotFound);
            if (!_usertRepository.AsNoTracking().Any(_ => model.UserId == _.Id))
                ThrowModelError(nameof(model.UserId), MessageError.DataNotFound);

            //var carts = Mapper.Map<Carts>(model);
            //carts.UserId = model.UserId;
            //carts.ProductId = model.ProductId;
            //carts.Size = model.Size;
            //carts.Quantity = model.Quantity;

            //await UnitOfWork.BeginTransactionAsync();
            //await _cartRepository.InsertAsync(carts);
            //await UnitOfWork.SaveChangesAsync();
            //return Mapper.Map<CartDto>(carts);

            // Kiểm tra xem sản phẩm đã tồn tại trong giỏ hàng chưa
            var existingCartItem = _cartProductRepository.AsNoTracking().Where(c => c.ProductId == model.ProductId && c.Size == model.Size).FirstOrDefault();

            if (existingCartItem != null)
            {
                // Nếu sản phẩm đã tồn tại, cập nhật số lượng
                existingCartItem.Quantity += model.Quantity.Value;
            }
            else
            {
                // Nếu sản phẩm chưa tồn tại, thêm mới một sản phẩm vào giỏ hàng
                var cart = Mapper.Map<CartProduct>(model);
                await UnitOfWork.BeginTransactionAsync();
                await _cartProductRepository.InsertAsync(cart);
            };
            await UnitOfWork.SaveChangesAsync(); // Lưu thay đổi sau khi thêm sản phẩm vào giỏ hàng

            // Trả về thông tin giỏ hàng sau khi thêm sản phẩm
            var updatedCartDto = new CartDto
            {
                UserId = model.UserId,
                ProductId = model.ProductId,
                Size = model.Size,
                Quantity = model.Quantity
            };

            return Mapper.Map<CartDto>(updatedCartDto);
        }

        public async Task<CartDto> CartAsync(Guid Id)
        {
            throw new NotImplementedException();
            //var cartItems = _cartRepository.AsNoTracking().Where(x => x.UserId == Id).GroupBy(x => new { x.ProductId, x.Size }).ToList();
            //var result = new CartDto
            //{

            //};
            //return new CartDto
            //{
            //    UserId = Id,
            //    //Items = cartItems
            //};
        }
    }
}
