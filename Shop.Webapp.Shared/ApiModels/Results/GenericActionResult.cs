namespace Shop.Webapp.Shared.ApiModels.Results
{
    public class GenericActionResult : GenericActionResult<object>
    {
        public GenericActionResult(string message) : base(message)
        { }
    }

    public class GenericActionResult<TData>
    {
        internal GenericActionResult()
        {
            Message = string.Empty;
        }

        public GenericActionResult(string message)
        {
            Message = message;
        }

        public GenericActionResult(string message, TData data)
        {
            Message = message;
            Data = data;
        }

        public string Message { get; internal set; }
        public TData Data { get; internal set; }
    }
}
