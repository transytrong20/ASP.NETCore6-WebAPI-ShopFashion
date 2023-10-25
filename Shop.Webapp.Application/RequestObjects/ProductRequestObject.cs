using Microsoft.AspNetCore.Http;
using Shop.Webapp.Shared.ApiModels.Requests;

namespace Shop.Webapp.Application.RequestObjects
{
    public class CreateOrUpdateProductModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; } 
        public IFormFile? FileUpLoad { get; set; }
        public int? Status { get; set; }
        public int? Discount { get; set; }
        public Guid? CategoryId { get; set; }
        public bool? Accepted { get; set; }
        public int? Index { get; set; }
    }

    public class OrderInformationPagingFilter : GenericPagingFilter
    {
        public Guid? CategoryId { get; set; }
        public bool? Accepted { get; set; }
    }

    public class OrderInformationPagingUserFilter : GenericPagingFilter
    {
        public Guid? CategoryId { get; set; }
    }
}
