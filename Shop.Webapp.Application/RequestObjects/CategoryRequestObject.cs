using Shop.Webapp.Shared.ApiModels.Requests;

namespace Shop.Webapp.Application.RequestObjects
{
    public class CreateOrUpdateCategoryModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Index { get; set; }
    }

    public class CategoryPagingFilter : GenericPagingFilter
    {
        public Guid? CategoryId { get; set; }
    }
}
