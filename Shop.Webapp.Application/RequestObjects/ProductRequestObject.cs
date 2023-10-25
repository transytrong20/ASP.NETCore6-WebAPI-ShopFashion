using Microsoft.AspNetCore.Http;

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
    }
}
