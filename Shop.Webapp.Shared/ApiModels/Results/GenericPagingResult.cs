using Shop.Webapp.Shared.ApiModels.Requests;

namespace Shop.Webapp.Shared.ApiModels.Results
{
    public class GenericPagingResult : GenericPagingResult<object>
    {
        public GenericPagingResult(int total, ICollection<object> data, GenericPagingRequest pagingRequest) : base(total, data, pagingRequest)
        {
        }
    }

    public class GenericPagingResult<TData> : GenericActionResult<IEnumerable<TData>>
    {
        public GenericPagingResult(int total, IEnumerable<TData> data, GenericPagingRequest pagingRequest)
        {
            Total = total;
            Data = data;
            PagingRequest = pagingRequest;
        }

        public int Total { get; internal set; }
        public GenericPagingRequest PagingRequest { get; set; }
    }
}
