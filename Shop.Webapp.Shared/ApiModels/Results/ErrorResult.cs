namespace Shop.Webapp.Shared.ApiModels.Results
{
    public class ErrorResult
    {
        public ErrorResult(string message)
        {
            Message = message;
            Errors = new Dictionary<string, string>();
        }

        public ErrorResult(string message, IDictionary<string, string> erros)
        {
            Message = message;
            Errors = erros;
        }

        public string Message { get; set; }
        public IDictionary<string, string> Errors { get; set; }
    }
}
