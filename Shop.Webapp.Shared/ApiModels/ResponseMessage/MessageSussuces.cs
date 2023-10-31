namespace Shop.Webapp.Shared.ApiModels.ResponseMessage
{
    public class MessageSuccess<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
}
