using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Webapp.Application.Dto;
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
    public class CategoryService : AppService, ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly CreateOrUpdateCategoryValidator _categoryValidator;
        public CategoryService(
            IRepository<Category> categoryRepository,
            CreateOrUpdateCategoryValidator categoryValidator,
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser,
            IMapper mapper) : base(unitOfWork, currentUser, mapper)
        {
            _categoryRepository = categoryRepository;
            _categoryValidator = categoryValidator;
        }

        public async Task<CategoryDto> CreateAsync(CreateOrUpdateCategoryModel model)
        {
            var valid = _categoryValidator.Validate(model);
            if (!valid.IsValid)
                ThrowValidate(valid.Errors);

            var category = Mapper.Map<Category>(model);
            await _categoryRepository.InsertAsync(category);

            return Mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto[]> GetAllAsync()
        {
            var categories = _categoryRepository.AsNoTracking().OrderBy(x=>x.Index).ToArray();
            return categories.Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Index = x.Index,
            }).ToArray();
        }

        public async Task<GenericPagingResult<CategoryDto>> GetPagingAsync(CategoryPagingFilter filter)
        {
            var query = _categoryRepository.AsNoTracking().AsQueryable();
            if (filter.CategoryId.HasValue)
                query = query.Where(x => x.Id == filter.CategoryId);

            var total = query.Count();
            var paged = query.OrderBy().PageBy(filter).ToArray();
            if (filter.Direction == "desc")
                paged = query.OrderBy(x => x.Index).PageBy(filter).ToArray();
            else
                paged = query.OrderByDescending(x => x.Index).PageBy(filter).ToArray();
            var data = paged.Select(x =>
            {
                return new CategoryDto
                {
                    Name = x.Name,
                    Description = x.Description,
                    Index = x.Index,
                };
            });

            return new GenericPagingResult<CategoryDto>(total, data, filter);
        }

        public async Task RemoveAsync(Guid id)
        {
            var category = await _categoryRepository.FindAsync(id);
            if (category == null)
                throw new NotFoundException(MessageError.NotFound);
            await UnitOfWork.BeginTransactionAsync();
            await _categoryRepository.DeleteAsync(category);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<CategoryDto> UpdateAsync(Guid id, CreateOrUpdateCategoryModel model)
        {
            var category = await _categoryRepository.FindAsync(id);
            if (category == null)
                throw new NotFoundException(MessageError.NotFound);

            ModelCheckIfNull<CreateOrUpdateCategoryModel, Category>.CheckIfNull(model, category);

            Mapper.Map(model, category);
            await _categoryRepository.UpdateAsync(category);
            return Mapper.Map<CategoryDto>(category);
        }

        public async Task<string> UpdateIndexAsync(Guid indexId, int index)
        {
            var arr = await _categoryRepository.AsNoTracking().Where(x => x.Index >= index || x.Id == indexId).ToArrayAsync();
            if (!arr.Any(x => x.Id == indexId))
                throw new NotFoundException(MessageError.NotFound);
            arr = arr.Select(x =>
            {
                if (x.Id == indexId)
                {
                    x.Index = index;
                }

                var check = _categoryRepository.AsNoTracking().Where(x => x.Id == indexId).Select(x => x.Index);
                if (x.Index <= check.FirstOrDefault() && x.Id != indexId)
                {
                    x.Index++;
                }
                return x;
            }).ToArray();

            try
            {
                await _categoryRepository.UpdateManyAsync(arr);
                await _categoryRepository.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Error updating database: " + MessageError.NotFound);
            }
            return ActionMessage.Update;
        }
    }
}
