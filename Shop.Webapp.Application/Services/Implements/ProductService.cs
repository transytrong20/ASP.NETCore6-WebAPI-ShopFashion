using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Webapp.Application.Dto;
using Shop.Webapp.Application.Helpers;
using Shop.Webapp.Application.RequestObjects;
using Shop.Webapp.Application.Services.Abstracts;
using Shop.Webapp.Application.Validators;
using Shop.Webapp.Domain;
using Shop.Webapp.EFcore.Repositories.Abstracts;
using Shop.Webapp.Shared.ApiModels;
using Shop.Webapp.Shared.ApiModels.CheckIfNull;
using Shop.Webapp.Shared.ApiModels.Results;
using Shop.Webapp.Shared.ConstsDatas;
using Shop.Webapp.Shared.Exceptions;

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
            var categoryName = _categoryRepository.AsNoTracking().Where(x => x.Id == model.CategoryId).Select(x => x.Name).FirstOrDefault();
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

        public async Task<string> UpdateIndexAsync(Guid indexId, int index)
        {
            var arr = await _productRepository.AsNoTracking().Where(x => x.Index >= index || x.Id == indexId).ToArrayAsync();
            if (!arr.Any(x => x.Id == indexId))
                throw new NotFoundException(MessageError.NotFound);
            arr = arr.Select(x =>
            {
                if (x.Id == indexId)
                {
                    x.Index = index;
                }

                var check = _productRepository.AsNoTracking().Where(x => x.Id == indexId).Select(x => x.Index);
                if (x.Index <= check.FirstOrDefault() && x.Id != indexId)
                {
                    x.Index++;
                }
                return x;
            }).ToArray();

            try
            {
                await _productRepository.UpdateManyAsync(arr);
                await _productRepository.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Error updating database: " + MessageError.NotFound);
            }
            return ActionMessage.Update;
        }

        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.FindAsync(id);
            if (product == null)
                throw new NotFoundException(MessageError.NotFound);

            var result = Mapper.Map<ProductDto>(product);

            var categoryId = _categoryProductRepository.AsNoTracking().Where(_ => _.ProductId == id).Select(_ => _.CategoryId).FirstOrDefault();
            result.Name = product.Name;
            result.Description = product.Description;
            result.Price = product.Price;
            result.Image = product.Image;
            result.Status = product.Status;
            result.Discount = product.Discount;
            result.CategoryId = categoryId;
            result.CategoryName = _categoryRepository.AsNoTracking().Where(x => x.Id == categoryId).Select(x => x.Name).ToArray();

            return result;
        }

        public async Task RemoveAsync(Guid id)
        {
            var product = await _productRepository.FindAsync(id);
            if (product == null)
                throw new NotFoundException(MessageError.NotFound);

            await UnitOfWork.BeginTransactionAsync();
            await _categoryProductRepository.DeleteDirectAsync(_ => _.ProductId == id);
            await _productRepository.DeleteAsync(product);

            await UnitOfWork.SaveChangesAsync();

            FileHelper.RemoveFile(product.Image);
        }

        public async Task<ProductDto> UpdateAsync(Guid id, CreateOrUpdateProductModel model)
        {
            var product = await _productRepository.FindAsync(id);
            if (product == null)
                throw new NotFoundException(MessageError.NotFound);

            ModelCheckIfNull<CreateOrUpdateProductModel, Product>.CheckIfNull(model, product);

            if (model.CategoryId == null)
            {
                var category = await _categoryProductRepository.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == id);
                if (category != null)
                {
                    model.CategoryId = category.CategoryId;
                }
            }

            if (!_categoryRepository.AsNoTracking().Any(_ => model.CategoryId == _.Id))
                ThrowModelError(nameof(model.CategoryId), MessageError.DataNotFound);

            var categoryName = _categoryRepository.AsNoTracking().Where(x => x.Id == model.CategoryId).Select(x => x.Name).FirstOrDefault();
            if (model.FileUpLoad != null)
            {
                var img = FileHelper.UploadFile(model.FileUpLoad, $"{categoryName}", $"{model.Name}-{model.FileUpLoad.FileName}");
                FileHelper.RemoveFile(product.Image);
                product.Image = img;
            }

            Mapper.Map(model, product);
            await UnitOfWork.BeginTransactionAsync();
            await _categoryProductRepository.DeleteDirectAsync(_ => _.ProductId == id);
            await _productRepository.UpdateAsync(product);
            await _categoryProductRepository.InsertAsync(new CategoryProduct()
            {
                ProductId = product.Id,
                CategoryId = model.CategoryId.Value
            });
            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<ProductDto>(product);
        }

        public async Task<GenericPagingResult<ProductDto>> GetPagingAsync(OrderInformationPagingFilter filter)
        {
            var query = _productRepository.AsNoTracking()
                .Include(x => x.Categories).ThenInclude(x => x.Category).AsQueryable();

            if (filter.CategoryId.HasValue)
                query = query.Where(x => x.Categories.Any(c => c.CategoryId == filter.CategoryId));

            if (filter.Accepted.HasValue)
                query = query.Where(x => x.Accepted == filter.Accepted.Value);

            var total = query.Count();
            var paged = query.OrderBy().PageBy(filter).ToArray();
            if (filter.Direction == "desc")
                paged = query.OrderBy(x => x.Index).PageBy(filter).ToArray();
            else
                paged = query.OrderByDescending(x => x.Index).PageBy(filter).ToArray();
            var data = paged.Select(x =>
            {
                var categories = x.Categories;
                var categoryId = categories.Select(c => c.CategoryId).FirstOrDefault();
                return new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Image = x.Image,
                    Status = x.Status,
                    Discount = x.Discount,
                    Accepted = x.Accepted,
                    Index = x.Index,
                    Sale = x.Sale,
                    New = x.New,
                    CategoryId = categoryId,
                    CategoryName = _categoryRepository.AsNoTracking().Where(x => x.Id == categoryId).Select(x => x.Name).ToArray()
                };
            });

            return new GenericPagingResult<ProductDto>(total, data, filter);
        }

        public List<ProductDto> GetlistProduct()
        {
            List<ProductDto> result = new List<ProductDto>();
            var product = _productRepository.AsNoTracking().OrderBy(x => x.Index).Take(6).ToList();
            foreach (var p in product)
            {
                ProductDto list = new ProductDto();
                list.Id= p.Id;
                list.Name = p.Name;
                list.Description = p.Description;
                list.Price = p.Price;
                list.Image = p.Image;
                list.Status = p.Status;
                list.Discount = p.Discount;
                list.Accepted = p.Accepted;
                list.Index = p.Index;
                list.New = p.New;
                list.Sale = p.Sale;
                list.SaleTurn = p.SaleTurn;

                result.Add(list);
            }
            return result;
        }

        public List<ProductDto> GetlistProductSaleTurn()
        {
            List<ProductDto> result = new List<ProductDto>();
            var product = _productRepository.AsNoTracking().OrderByDescending(p => p.Sold).ToList().Take(6);
            foreach (var p in product)
            {
                ProductDto list = new ProductDto();
                list.Id = p.Id;
                list.Name = p.Name;
                list.Description = p.Description;
                list.Price = p.Price;
                list.Image = p.Image;
                list.Status = p.Status;
                list.Discount = p.Discount;
                list.Accepted = p.Accepted;
                list.Index = p.Index;
                list.New = p.New;
                list.Sale = p.Sale;
                list.SaleTurn = p.SaleTurn;

                result.Add(list);
            }
            return result;
        }

        public List<ProductDto> SimilarProduct(Guid categoryId)
        {
            List<ProductDto> result = new List<ProductDto>();
            var product = _productRepository.AsNoTracking().Include(x=> x.Categories).Where(x => x.Categories.Any(c => c.CategoryId == categoryId)).OrderBy(x => x.Sold).Take(4).ToList();
            foreach (var p in product)
            {
                ProductDto list = new ProductDto();
                list.Id = p.Id;
                list.Name = p.Name;
                list.Description = p.Description;
                list.Price = p.Price;
                list.Image = p.Image;
                list.Status = p.Status;
                list.Discount = p.Discount;
                list.Accepted = p.Accepted;
                list.Index = p.Index;
                list.New = p.New;
                list.Sale = p.Sale;
                list.SaleTurn = p.SaleTurn;

                result.Add(list);
            }
            return result;
        }
    }
}
