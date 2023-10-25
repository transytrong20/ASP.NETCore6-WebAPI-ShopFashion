namespace Shop.Webapp.Shared.ApiModels.Requests
{
    public class GenericOkResult<TData>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public TData Data { get; set; }

        public GenericOkResult(int statusCode, string message, TData data = default)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
    }
    public class GenericOkResult
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public GenericOkResult(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
