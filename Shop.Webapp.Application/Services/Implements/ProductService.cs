using AutoMapper;
using Shop.Webapp.Application.Dto;
using Shop.Webapp.Application.Helpers;
using Shop.Webapp.Application.RequestObjects;
using Shop.Webapp.Application.Services.Abstracts;
using Shop.Webapp.Application.Validators;
using Shop.Webapp.Domain;
using Shop.Webapp.EFcore.Repositories.Abstracts;
using Shop.Webapp.Shared.ApiModels;
using Shop.Webapp.Shared.ConstsDatas;

namespace Shop.Webapp.Application.Services.Implements
{
    public class ProductService : AppService, IProductService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<CategoryProduct> _categoryProductRepository;
        private readonly CreateOrUpdateProductValidator _productValidator;
        public ProductService(
            IRepository<Category> categoryRepository,
            IRepository<Product> productRepository,
            IRepository<CategoryProduct> categoryProductRepository,
            CreateOrUpdateProductValidator productValidator,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            IMapper mapper) : base(unitOfWork, currentUser, mapper)

        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _categoryProductRepository = categoryProductRepository;
            _productValidator = productValidator;
        }

        public async Task<ProductDto> CreateAsync(CreateOrUpdateProductModel model)
        {
            var valid = _productValidator.Validate(model);
            if (!valid.IsValid)
                ThrowValidate(valid.Errors);

            if (!_categoryRepository.AsNoTracking().Any(_ => model.CategoryId == _.Id))
            {
                ThrowModelError(nameof(model.CategoryId), MessageError.DataNotFound);
            }
            var categoryName = _categoryRepository.AsNoTracking().Where(x=> x.Id == model.CategoryId).Select(x=> x.Name).FirstOrDefault();
            var image = string.Empty;
            if (model.FileUpLoad != null)
            {
                image = FileHelper.UploadFile(model.FileUpLoad, $"{categoryName}", $"{model.Name}-{model.FileUpLoad.FileName}");
            }
            var product = Mapper.Map<Product>(model);
            product.Image = image;

            await UnitOfWork.BeginTransactionAsync();
            await _productRepository.InsertAsync(product);
            await _categoryProductRepository.InsertAsync(new CategoryProduct()
            {
                ProductId = product.Id,
                CategoryId = model.CategoryId.Value
            });

            await UnitOfWork.SaveChangesAsync();
            return Mapper.Map<ProductDto>(product);
        }
    }
}
