using System.Net;

namespace Shop.Webapp.Shared.Exceptions
{
    public class CustomerException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public Dictionary<string, string> Errors { get; set; }

        public CustomerException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.BadRequest;
            Errors = new Dictionary<string, string>();
        }

        public CustomerException(string message, Dictionary<string, string> errors) : base(message)
        {
            StatusCode = HttpStatusCode.BadRequest;
            Errors = errors;
        }
    }
}
