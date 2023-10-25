namespace Shop.Webapp.Shared.ApiModels.Requests
{
    public class GenericPagingFilter : GenericPagingRequest
    {
        public string? SearchKey { get; set; }
        public bool? Accepted { get; set; }
    }
}
