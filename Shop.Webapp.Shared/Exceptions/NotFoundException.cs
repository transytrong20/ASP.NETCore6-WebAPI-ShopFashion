using System.Net;

namespace Shop.Webapp.Shared.Exceptions
{
    public class NotFoundException : CustomerException
    {
        public NotFoundException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}
