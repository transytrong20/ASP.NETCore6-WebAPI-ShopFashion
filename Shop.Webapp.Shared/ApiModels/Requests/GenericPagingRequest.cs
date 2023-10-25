namespace Shop.Webapp.Shared.ApiModels.Requests
{
    public class GenericPagingRequest
    {
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;

        public string SortBy { get; set; } = string.Empty;
        public string Direction { get; set; } = "desc";
    }
}
