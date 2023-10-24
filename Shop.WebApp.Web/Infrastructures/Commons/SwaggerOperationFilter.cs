using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Shop.Webapp.Shared.ConstsDatas;
using Shop.Webapp.Shared.Results;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shop.WebApp.Web.Infrastructures.Commons
{
    public class SwaggerOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var validateErr = new Dictionary<string, string>();
            validateErr.Add("PropertyA", MessageError.UnValid);
            validateErr.Add("PropertyB", MessageError.NotEmpty);
            var badRequestExample = new ErrorResult("ErrorValidate", validateErr);

            operation.Responses.Add("400", new OpenApiResponse
            {
                Description = "Bad Request",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Example = new OpenApiString(JsonConvert.SerializeObject(badRequestExample))
                    }
                }
            });

            operation.Responses.Add("404", new OpenApiResponse
            {
                Description = "Not Found",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Example = new OpenApiString(JsonConvert.SerializeObject(new ErrorResult(MessageError.NotFound)))
                    }
                }
            });

            operation.Responses.Add("500", new OpenApiResponse
            {
                Description = "Server Internal",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Example = new OpenApiString(JsonConvert.SerializeObject(new ErrorResult("Server Internal")))
                    }
                }
            });
        }
    }
}
